using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    public float speed = 4f;
    public float waitTime = 1f;
    public Transform point;
    bool isWait = false;
    bool isHidden = false;

    private void Start()
    {
        //point - it's our destination
        point.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
    }

    private void Update()
    {
        //if not waint - move towards point
        if (isWait == false)
            transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

        //if we on the place
        if (transform.position == point.position)
        {
            if (isHidden)
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                isHidden = false;
            }
            else
            { 
                point.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                isHidden = true;
            }
            isWait = true;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
    }
}
