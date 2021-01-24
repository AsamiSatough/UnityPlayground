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

    public void Start()
    {
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
}
