using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x -= 5f;
        transform.localPosition = pos;

        // if(transform.localPosition.x < -612f)
        // {
        //     Destroy (gameObject);
        // }
    }
}
