using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float jumpOnDestroy = 4f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpOnDestroy, ForceMode2D.Impulse);
            gameObject.GetComponentInParent<Enemy>().Death();
        }
    }
}
