using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    bool isDead;
    float afterDeadTime;
    PlayerController pc;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        isDead = false;
        afterDeadTime = 0;
    }
    private void Update()
    {
        if (isDead)
        {
            this.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(-100.0f, 100.0f));

            afterDeadTime += Time.deltaTime;
            if (afterDeadTime > 0.5f)
                gameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (pc.checkBreakSkill())
        {
            Destroy(target.gameObject);
            AudioManager.Instance.PlaySE("Hit");
        }
        else
        {
            isDead = true;
            AudioManager.Instance.PlaySE("Hit");
        }
    }


    private void gameOver()
    {
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(this.invokeActionOnloadScene("ResultScore", () =>
        {
            var scoreResult = FindObjectOfType<ScoreResult>() as ScoreResult;
            scoreResult.kyori = (int)gc.GameScore;
            scoreResult.time = gc.GameTime;
            scoreResult.SetScore((int)gc.GameScore);
            scoreResult.characterName = pc.getCharacterName();
            scoreResult.rarity = pc.getRarity();
        }));
        isDead = false;
    }
    private IEnumerator invokeActionOnloadScene(string sceneName, System.Action onLoad)
    {
        var asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return asyncOp;
        onLoad.Invoke();
        SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
