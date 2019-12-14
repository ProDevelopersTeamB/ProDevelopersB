using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    float jumpPower;
    GameObject animal;

    const float FLAP = 650f;
    bool jump = false;

    private void Start()
    {
        if (animal != null)
        {
            GameObject obj = Instantiate(animal);
            obj.transform.position = this.transform.position;
            obj.transform.parent = this.transform;
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown("space") && !jump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (FLAP + jumpPower * 3));
            jump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        jump = false;
    }

    public void setParam(float s, float j)
    {
        Speed = s;
        jumpPower = j;
    }
    public void setAnimal(GameObject ani)
    {
        animal = ani;
        Instantiate(animal);
    }
}
