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
        SceneManager.LoadScene("ResultScore");
    }
}
