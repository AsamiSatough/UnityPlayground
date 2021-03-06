using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float jumpHeight = 8;
    bool enemyKilled = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !enemyKilled)
        {
            collision.gameObject.GetComponent<Player>().RecountHp(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    public IEnumerator DeathCoroutine()
    {
        enemyKilled = true;

        GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;

        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void Death()
    {
        StartCoroutine(DeathCoroutine());
    }
}
