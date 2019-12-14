using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float rarity;
    public float speed;
    public float jump;
    float fixedSpeed;
    float fixedJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(float s)
    {
        fixedSpeed = s;
    }
    public void SetJump(float j)
    {
        fixedJump = j;
    }
}
