using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatrol : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2.0f;

    private void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, point1.position.z);
        StartCoroutine(PausedFly());
    }

    private void Update()
    {
      
    }

    IEnumerator PausedFly()
    {
        transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);

        if (transform.position == point1.position)
        {
            Transform t = point1;
            yield return new WaitForSeconds(2.0f);

            point1 = point2;
            point2 = t;
        }
    }
}
