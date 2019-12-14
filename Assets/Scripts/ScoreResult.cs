using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreResult : MonoBehaviour
{
    public int kyori;
    public float time;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Result");
        string text = "キョリ: " + kyori + "\n\n";
        text += "タイム: " + time + " sec.\n\n";
        text += "とくてん: " + score + "\n\n\n";
        text += "ひょうか: " + "S" + "\n\n";
        GetComponent<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("GashaScene");
    }
}
