using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaLogic : MonoBehaviour
{
    public List<GameObject> characters;
    Text resultText;

    enum State
    {
        Idle = 0,
        Lottery = 1,
        Result = 2,
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        resultText = GameObject.Find("ResultText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Lottery)
        {
            int rarityRand = Random.Range(1, 100);
            int rarity;
            if (rarityRand <= 5)
            {
                rarity = 5;
            } else if (rarityRand <= 15)
            {
                rarity = 4;

            }
            else if (rarityRand <= 30)
            {
                rarity = 3;
            }
            else if (rarityRand <= 60)
            {
                rarity = 2;
            }
            else
            {
                rarity = 1;
            }

            int charaRand = Random.Range(0, characters.Count - 1);
            GameObject resultObject = characters[charaRand];
            Character resultCharacter = resultObject.GetComponent<Character>();
            resultCharacter.rarity = rarity;
            Instantiate(resultObject);
            string text = "レアリティ:";
            for (int i=0; i < resultCharacter.rarity; i++)
            {
                text += "★";
            }
            text += "\nスピード: " + resultCharacter.speed;
            text += "\nジャンプ: " + resultCharacter.jump;
            resultText.text = text;
            state = State.Result;
        }
    }

    public void StartGacha()
    {
        state = State.Lottery;
    }
}
