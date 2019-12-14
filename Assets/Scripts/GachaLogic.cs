using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaLogic : MonoBehaviour
{
    public List<GameObject> characters;
    Text resultText;
    float drumRollPosX;
    float time;

    enum State
    {
        Idle = 0,
        DrumRollInit,
        DrumRoll,
        Lottery,
        Result,
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        drumRollPosX = -6.0f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        switch (state)
        {
            case State.DrumRollInit:
                resultText = GameObject.Find("ResultText").GetComponent<Text>();
                for (int i=0; i<5; i++)
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
                if (!AudioManager.Instance.IsPlayingSE())
                {
                    AudioManager.Instance.PlaySE("RollFinish");
                    state = State.Lottery;
                    foreach (Transform n in this.transform)
                    {
                        GameObject.Destroy(n.gameObject);
                    }
                }


                break;
            case State.Lottery:
                int rarityRand = Random.Range(1, 100);
                int rarity;
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

                int charaRand = Random.Range(0, characters.Count - 1);
                GameObject resultObject = characters[charaRand];
                Character resultCharacter = resultObject.GetComponent<Character>();
                resultCharacter.rarity = rarity;
                resultCharacter.speed *= coefficient;
                resultCharacter.jump *= coefficient;

                Instantiate(resultObject);
                string text = "レアリティ:";
                for (int i = 0; i < resultCharacter.rarity; i++)
                {
                    text += "★";
                }
                text += "\nスピード: " + resultCharacter.speed;
                text += "\nジャンプ: " + resultCharacter.jump;
                resultText.text = text;
                state = State.Result;
                break;
            default:
                break;
        }
    }

    public void StartGacha()
    {
        AudioManager.Instance.PlaySE("DrumRoll");
        state = State.DrumRollInit;
    }
}
