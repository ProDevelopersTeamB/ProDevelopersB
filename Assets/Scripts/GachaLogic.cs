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
        Roulette = 1,
        Lottery = 2,
        Result = 3,
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
            float coefficient;
            if (rarityRand <= 5)
            {
                rarity = 5;
                coefficient = 1.5f;
            } else if (rarityRand <= 15)
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
