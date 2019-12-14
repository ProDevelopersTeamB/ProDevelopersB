using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D target)
    {
        gameOver();
    }

    private void gameOver()
    {
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(this.invokeActionOnloadScene("ResultScore", () => {
            var scoreResult = FindObjectOfType<ScoreResult>() as ScoreResult;
            scoreResult.kyori = (int)gc.GameScore;
            scoreResult.time = gc.GameTime;
            scoreResult.score = (int)gc.GameScore;
        }));

    }
    private IEnumerator invokeActionOnloadScene(string sceneName, System.Action onLoad)
    {
        var asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return asyncOp;
        onLoad.Invoke();
        SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
