using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 4f;
    //float timeToDisable = 4f;

    private void Start()
    {
        StartCoroutine(SetDisable());
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);    
    }

    private IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(speed);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisable());
        gameObject.SetActive(false);
        
    }
}
