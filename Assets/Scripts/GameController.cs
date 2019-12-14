using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private ObstacleController obstacleController;
    [SerializeField] private FloorController floorController;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeText;
    public float GameScore;
    public float GameTime;
    
    private void Update()
    {
        GameScore += Time.deltaTime * 10;
        scoreText.text = "距離: " + (int)GameScore;

        GameTime += Time.deltaTime;
        timeText.text = "時間: " + (int)GameTime;

        if(GameTime > 60)
        {
            if(!obstacleController.Floor2Flg)
                StartCoroutine(obstacleController.CreateFloor2());
        }
        else if(!obstacleController.ObstacleFlg)
        {
            StartCoroutine(obstacleController.CreateObstacle());
        }
    }
}
