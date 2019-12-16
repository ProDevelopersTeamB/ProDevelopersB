using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    float jumpPower;
    bool isBig, isFly;
    GameObject animal;

    const float FLAP = 650f;
    bool jump = false;
    bool doubleJump = false;
    float jumpedTime;
    bool isUsedSkill;
    string characterName;
    int rarity;

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
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (FLAP + jumpPower * 10));
            jump = true;
            jumpedTime = 0;
        }
        else if (Input.GetKeyDown("space") && !doubleJump && isFly && jumpedTime >= 0.1f)
        {
            AudioManager.Instance.PlaySE("Jump");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (FLAP + jumpPower * 10));
            doubleJump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (jumpedTime >= 0.3f)
        {
            jump = false;
            doubleJump = false;
        }
    }

    public void setParam(float s, float j, int r, string n, bool isBig = false, bool isFly = false)
    {
        Speed = s;
        jumpPower = j;
        rarity = r;
        characterName = n;
        this.isBig = isBig;
        this.isFly = isFly;
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
    public string getCharacterName()
    {
        return characterName;
    }
    public int getRarity()
    {
        return rarity;
    }
}
