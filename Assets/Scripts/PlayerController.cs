using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float flap = 2f;
    bool jump = false;

    void Update () {
        if (Input.GetKeyDown("space") && !jump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * flap);
            jump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "floor")
            jump = false;
        else if(target.gameObject.tag == "floor2")
            gameOver();
        else if(target.gameObject.tag == "floor")
            gameOver();
    }

    private void gameOver()
    {
        Debug.Log("GameOver");
    }
}
