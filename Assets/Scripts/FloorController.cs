using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject canvas;
    [SerializeField] private PlayerController player;
    private GameObject tempFloor;
    private void Start()
    {
        tempFloor = Instantiate (floor, canvas.transform);
    }

    private void Update()
    {
        var pos = tempFloor.transform.localPosition;
        if(pos.x < 424f)
        {
            tempFloor = Instantiate (floor, canvas.transform);
            tempFloor.GetComponent<Floor>().Speed = player.Speed;
        }
    }
}
