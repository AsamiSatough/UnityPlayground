using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatrol : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 2.0f;
    public float waitTime = 3f;
    bool canGo = true;
    float _rotation = 0;

    private void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, point1.position.z);
    }

    private void Update()
    {
        if (canGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        }

        if (transform.position == point1.position)
        {
            Transform t = point1;
            point1 = point2;
            point2 = t;

            canGo = false;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        _rotation += 180;

        yield return new WaitForSeconds(waitTime);
        transform.eulerAngles = new Vector3(0, _rotation, 0);
        canGo = true;
    }
}
