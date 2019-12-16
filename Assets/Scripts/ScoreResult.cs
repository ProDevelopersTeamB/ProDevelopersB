using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ScoreResult : MonoBehaviour
{
    public string characterName;
    public int rarity;
    public int kyori;
    public float time;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBGM("Result", 0.5f);
        string text = "スコア: " + score;
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

    public void Tweet()
    {
        // キャラを渡す必要有り スコアはもうある
        // switchでcharacterNameとendingWordを更新
        // forでrarityTextを更新

        string rarityText = "";
        for (int i = 0; i < rarity; i++)
            rarityText += "★";
        string endingWord = "";
        switch (characterName)
        {
            case "うさぎ":
                endingWord = "ピョン!";
                break;
            case "にわとり":
                endingWord = "コケ!";
                break;
            case "ひよこ":
                endingWord = "ピヨ!";
                break;
            case "ひつじ":
                endingWord = "メェ!";
                break;
            case "ねこ":
                endingWord = "ニャン!";
                break;
            case "いぬ":
                endingWord = "ワン!";
                break;
            case "うし":
                endingWord = "モー!";
                break;
            case "ゴリラ":
                endingWord = "ウホ";
                break;
            case "とり":
                endingWord = "チュン!";
                break;
            case "へび":
                endingWord = "ニョロ!";
                break;
        }

        string url = "https://twitter.com/intent/tweet?text=" +
            UnityWebRequest.EscapeURL(characterName + "(" + rarityText + ")でスコア " + score + " を達成した" + endingWord + " #どうぶつサモンラン\nhttps://musasin.github.io/summon.html");
#if UNITY_EDITOR
        Application.OpenURL(url);
#elif UNITY_WEBGL
        Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
        Application.OpenURL(url);
#endif

    }
}
