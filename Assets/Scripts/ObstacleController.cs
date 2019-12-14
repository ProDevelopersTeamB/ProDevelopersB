using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject floor2;
    [SerializeField] private GameObject canvas;

    public bool ObstacleFlg;
    public bool Floor2Flg;

    // void Start()
    // {
        // Instantiate (floor2, canvas.transform);
    // }

    public IEnumerator CreateObstacle()
    {
        ObstacleFlg = true;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Instantiate (obstacle, canvas.transform);
        ObstacleFlg = false;
    }

    public IEnumerator CreateFloor2()
    {
        Floor2Flg = true;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        Instantiate (floor2, canvas.transform);
        Floor2Flg = false;
    }
}
