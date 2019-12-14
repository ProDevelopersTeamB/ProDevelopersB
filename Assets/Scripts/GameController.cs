using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
                AudioManager.Instance.PlayBGM("Run", 0.5f);
                break;
            case 2:
                AudioManager.Instance.PlayBGM("Run_2", 0.5f);
                break;
            case 3:
                AudioManager.Instance.PlayBGM("Run_3", 0.5f);
                break;
        }
    }

    private void Update()
    {
        GameScore += Time.deltaTime * player.Speed;
        scoreText.text = "SCORE: " + (int)GameScore;

        GameTime += Time.deltaTime;
        timeText.text = (int)GameTime + "(time) x " + player.Speed + "(speed)";

        int randMax = Mathf.Min(6, 2 + (int)(GameTime / 3));


        if (!obstacleController.CreateFlg)
        {
            switch (Random.Range(1, randMax))
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
                case 4:
                    StartCoroutine(obstacleController.CreateFloor4());
                    break;
                case 5:
                    StartCoroutine(obstacleController.CreateFloor5());
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

    public void Reset()
    {
        SceneManager.LoadScene("GashaScene");
    }
}
