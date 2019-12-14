using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController_bak : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x -= 5f;
        transform.localPosition = pos;

        if(transform.localPosition.x < -1536)
        {
            pos.x = 512f;
            transform.localPosition = pos;
        }
    }
}
