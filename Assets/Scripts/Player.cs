using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public Transform groundCheck;
    bool isGrounded;
    Animator anim;
    public int curHp;
    public int maxHp = 3;
    public main main;

    public bool key = false;

    public void Start()
    {
        curHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        CheckGround();

        //State 1 - idle
        //State 2 - walking
        //State 3 - jump

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);

        //("Horizontal") == 0 means that we don't move at all
        if (isGrounded)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                Flip();
                anim.SetInteger("State", 2);
            }
            else
                anim.SetInteger("State", 1);
        }
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround()
    {
        //OverlapCircleAll required point and its raduis, returns all Colliders2D
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);

        isGrounded = colliders.Length > 1;

        if (!isGrounded)
            anim.SetInteger("State", 3);
    }

    public void RecountHp(int deltaHp)
    {
        curHp += deltaHp;
        if (deltaHp < 0)
        {
            StartCoroutine(OnHitRed());
        }

        if (curHp <= 0)
        {
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 2f);
        }
    }

    IEnumerator OnHitRed()
    {
        int counter = 0;
        while (counter != 4)
        {   //become red
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.25f, GetComponent<SpriteRenderer>().color.b - 0.25f);
            yield return new WaitForSeconds(0.02f);
            counter++;
        }
        if (GetComponent<SpriteRenderer>().color.g == 0)
            while (GetComponent<SpriteRenderer>().color.g != 1)
            {   //become normal
                GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.25f, GetComponent<SpriteRenderer>().color.b + 0.25f);
                yield return new WaitForSeconds(0.02f);
            }
        StopCoroutine(OnHitRed());
    }

    void Lose()
    {
        main.GetComponent<main>().Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            key = true;

           
          
        }
        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<Door>().isOpen)
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);

            else if (key)
            {
                collision.gameObject.GetComponent<Door>().Unlock();
                key = false;
            }

        }

    }
}
