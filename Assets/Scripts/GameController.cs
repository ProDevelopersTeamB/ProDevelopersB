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
    [SerializeField] private PlayerController player;
    public float GameScore;
    public float GameTime;

    private void Start()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                AudioManager.Instance.PlayBGM("Run");
                break;
            case 2:
                AudioManager.Instance.PlayBGM("Run_2");
                break;
            case 3:
                AudioManager.Instance.PlayBGM("Run_3");
                break;
        }
    }

    private void Update()
    {
        GameScore += Time.deltaTime * (10f * (5f + player.Speed / 10f));
        scoreText.text = "距離: " + (int)GameScore;

        GameTime += Time.deltaTime;
        timeText.text = "時間: " + (int)GameTime;

        if(!obstacleController.CreateFlg)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    StartCoroutine(obstacleController.CreateObstacle());
                    break;
                case 2:
                    StartCoroutine(obstacleController.CreateFloor2());
                    break;
                case 3:
                    StartCoroutine(obstacleController.CreateFloor3());
                    break;
            }
        }

        // if(GameTime > 60)
        // {
        //     if(!obstacleController.Floor2Flg)
        //         StartCoroutine(obstacleController.CreateFloor2());
        // }
        // else if(!obstacleController.ObstacleFlg)
        // {
        //     StartCoroutine(obstacleController.CreateObstacle());
        // }
    }
}
