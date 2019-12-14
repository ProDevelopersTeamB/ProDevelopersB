using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    float jumpPower;
    bool isBig;
    GameObject animal;

    const float FLAP = 650f;
    bool jump = false;
    float jumpedTime;
    bool isUsedSkill;

    private void Start()
    {
        isUsedSkill = false;
        jumpedTime = 0;
        if (animal != null)
        {
            GameObject obj = Instantiate(animal);
            if (isBig)
                obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.12f, this.transform.position.z);
            else
                obj.transform.position = this.transform.position;
            obj.transform.parent = this.transform;
        }
    }

    void Update()
    {
        jumpedTime += Time.deltaTime;
        if (Input.GetKey("space") && !jump && jumpedTime >= 0.3f)
        {
            AudioManager.Instance.PlaySE("Jump");
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (FLAP + jumpPower * 10));
            jump = true;
            jumpedTime = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (jumpedTime >= 0.3f)
            jump = false;
    }

    public void setParam(float s, float j, bool isBig = false)
    {
        Speed = s;
        jumpPower = j;
        this.isBig = isBig;
    }
    public void setAnimal(GameObject ani)
    {
        animal = ani;
    }
    public bool checkBreakSkill()
    {
        if (isBig && !isUsedSkill)
        {
            isUsedSkill = true;
            return true;
        }
        return false;
    }
}
