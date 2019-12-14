using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreResult : MonoBehaviour
{
    public int kyori;
    public float time;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(score);
        AudioManager.Instance.PlayBGM("Result", 0.5f);
        Debug.Log(score);
        string text = "スコア: " + score;
        Debug.Log(score);
        /*
            string text = "キョリ: " + kyori + "\n\n";
        text += "タイム: " + time + " sec.\n\n";
        text += "とくてん: " + score + "\n\n\n";
        text += "ひょうか: " + "S" + "\n\n */
        GetComponent<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GoToTitle();
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("GashaScene");
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
}
