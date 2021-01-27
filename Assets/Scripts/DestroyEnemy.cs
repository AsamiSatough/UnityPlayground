using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    void Update()
    {
        bool yes = GetComponentInChildren<TouchToDestroy>().touched;
        if (yes)
            Destroy(gameObject, 2);
    }
}
