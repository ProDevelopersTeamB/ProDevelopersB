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
        StartCoroutine(this.invokeActionOnloadScene("ResultScore", () => {
            var scoreResult = FindObjectOfType<ScoreResult>() as ScoreResult;
            scoreResult.kyori = 10;
            scoreResult.time = 20;
            scoreResult.score = 30;
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
