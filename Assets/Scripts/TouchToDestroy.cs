using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToDestroy : MonoBehaviour
{
    public float jumpHeight = 6;
    public bool touched = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(GetComponentInParent<Enemy>());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);

            touched = true;
            GetComponentInParent<Animator>().SetBool("isDead", true);
            GetComponentInParent<Beetle>().enabled = false;
        }
    }
}
