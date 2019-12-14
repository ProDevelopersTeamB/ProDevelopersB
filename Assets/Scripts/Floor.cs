using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    const float SPEED = 5f;
    public float Speed;

    PlayerController pc;

    private void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    private void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x -= (5 + pc.Speed * 2 / 10f);
        transform.localPosition = pos;

        if (transform.localPosition.x < -612f)
        {
            Destroy(gameObject);
        }
    }
}
