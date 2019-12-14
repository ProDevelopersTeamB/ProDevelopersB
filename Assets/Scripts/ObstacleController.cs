using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject floor2;
    [SerializeField] private GameObject canvas;

    void Start()
    {
        Instantiate (floor2, canvas.transform);
    }
}
