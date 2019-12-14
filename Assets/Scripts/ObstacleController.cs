using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject floor2;
    [SerializeField] private GameObject floor3;
    [SerializeField] private GameObject floor4;
    [SerializeField] private GameObject floor5;
    [SerializeField] private GameObject canvas;
    [SerializeField] private PlayerController player;

    public bool CreateFlg;
    // public bool ObstacleFlg;
    // public bool Floor2Flg;
    // public bool Floor3Flg;

    // void Start()
    // {
    // Instantiate (floor2, canvas.transform);
    // }

    public IEnumerator CreateObstacle()
    {
        CreateFlg = true;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        var obstacleObj = Instantiate(obstacle, canvas.transform);
        obstacleObj.GetComponent<Obstacle>().Speed = player.Speed;
        CreateFlg = false;
    }

    public IEnumerator CreateFloor2()
    {
        CreateFlg = true;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        var floor2Obj = Instantiate(floor2, canvas.transform);
        floor2Obj.GetComponent<Obstacle>().Speed = player.Speed;
        CreateFlg = false;
    }

    public IEnumerator CreateFloor3()
    {
        CreateFlg = true;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        var floor3Obj = Instantiate(floor3, canvas.transform);
        floor3Obj.GetComponent<Obstacle>().Speed = player.Speed;
        CreateFlg = false;
    }
    public IEnumerator CreateFloor4()
    {
        CreateFlg = true;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        var floor4Obj = Instantiate(floor4, canvas.transform);
        floor4Obj.GetComponent<Obstacle>().Speed = player.Speed;
        CreateFlg = false;
    }
    public IEnumerator CreateFloor5()
    {
        CreateFlg = true;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        var floor5Obj = Instantiate(floor5, canvas.transform);
        floor5Obj.GetComponent<Obstacle>().Speed = player.Speed;
        CreateFlg = false;
    }
}
