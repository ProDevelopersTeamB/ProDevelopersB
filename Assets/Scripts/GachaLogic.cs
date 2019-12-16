using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaLogic : MonoBehaviour
{
    public GameObject syuutyuusen;
    public List<GameObject> characters;
    GameObject resultObject;
    Text resultText;
    float drumRollPosX;
    float time, resultTime, speed, jump;
    int rarity;
    string characterName;
    bool isBig, isFly;

    enum State
    {
        Idle = 0,
        DrumRollInit,
        DrumRoll,
        Lottery,
        Result,
        End,
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, false);

        AudioManager.Instance.PlayBGM("Title", 1.0f, true);
        state = State.Idle;
        drumRollPosX = -6.0f;
        time = 0;
        resultTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        switch (state)
        {
            case State.DrumRollInit:
                resultText = GameObject.Find("ResultText").GetComponent<Text>();
                for (int i = 0; i < 8; i++)
                {
                    foreach (GameObject charaObj in characters)
                    {
                        GameObject obj = Instantiate(charaObj);
                        obj.transform.parent = this.transform;
                        obj.transform.position = new Vector2(drumRollPosX, 0);
                        obj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                        drumRollPosX += 2.0f;
                    }
                }
                state = State.DrumRoll;
                break;
            case State.DrumRoll:
                this.transform.position = new Vector2(this.transform.position.x - 0.2f, 0);
                if (!AudioManager.Instance.IsPlayingSE() || Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
                {
                    Instantiate(syuutyuusen);
                    AudioManager.Instance.PlaySE("RollFinish");
                    state = State.Lottery;
                    foreach (Transform n in this.transform)
                    {
                        GameObject.Destroy(n.gameObject);
                    }
                }


                break;
            case State.Lottery:
                int rarityRand = Random.Range(1, 101);
                float coefficient;
                if (rarityRand <= 5)
                {
                    rarity = 5;
                    coefficient = 1.5f;
                }
                else if (rarityRand <= 15)
                {
                    rarity = 4;
                    coefficient = 1.25f;
                }
                else if (rarityRand <= 30)
                {
                    rarity = 3;
                    coefficient = 1.0f;
                }
                else if (rarityRand <= 60)
                {
                    rarity = 2;
                    coefficient = 0.75f;
                }
                else
                {
                    rarity = 1;
                    coefficient = 0.5f;
                }

                int charaRand = Random.Range(0, characters.Count);
                resultObject = characters[charaRand];
                Character resultCharacter = resultObject.GetComponent<Character>();
                resultCharacter.rarity = rarity;
                speed = resultCharacter.speed * coefficient;
                jump = resultCharacter.jump * coefficient;
                isBig = resultCharacter.isBig;
                isFly = resultCharacter.isFly;
                resultCharacter.SetSpeed(speed);
                resultCharacter.SetJump(jump);
                characterName = resultCharacter.characterName;

                Instantiate(resultObject);
                string text = "レアリティ:";
                for (int i = 0; i < resultCharacter.rarity; i++)
                {
                    text += "★";
                }
                text += "\nスピード: " + speed;
                text += "\nジャンプ: " + jump;
                if (isBig)
                {
                    text += "\nスキル: 1 回だけブロックを破壊";
                }
                if (isFly)
                {
                    text += "\nスキル: 2 回までジャンプできる";
                }
                resultText.text = text;
                state = State.Result;
                resultTime = 0;
                break;
            case State.Result:
                resultTime += Time.deltaTime;
                if (resultTime > 3.0f || Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
                {
                    StartCoroutine(this.invokeActionOnloadScene("Run", () =>
                    {
                        var player = FindObjectOfType<PlayerController>() as PlayerController;
                        player.setParam(speed, jump, rarity, characterName, isBig, isFly);
                        player.setAnimal(resultObject);
                    }));
                    state = State.End;
                }
                break;
            default:
                break;
        }
    }
    private IEnumerator invokeActionOnloadScene(string sceneName, System.Action onLoad)
    {
        var asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return asyncOp;
        onLoad.Invoke();
        SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGacha()
    {
        AudioManager.Instance.PlayBGM("Gasha", 1.0f, true);
        AudioManager.Instance.PlaySE("DrumRoll");
        AudioManager.Instance.PlaySE("Decide");
        state = State.DrumRollInit;
    }
}
