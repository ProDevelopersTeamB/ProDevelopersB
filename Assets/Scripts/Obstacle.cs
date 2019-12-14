using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    const float SPEED = 5f;
    public float Speed;

    private void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x -= (SPEED + Speed / 10f);
        transform.localPosition = pos;

        // if(transform.localPosition.x < -612f)
        // {
        //     Destroy (gameObject);
        // }
    }
}
