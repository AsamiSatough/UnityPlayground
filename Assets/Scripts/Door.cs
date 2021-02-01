using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Transform door;
    public Sprite mid, top;

    public void Unlock()
    {
        isOpen = true;
        GetComponent<SpriteRenderer>().sprite = mid;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = top;

        door.GetComponent<Door>().GetComponent<SpriteRenderer>().sprite = mid;
        door.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = top;
        door.GetComponent<Door>().isOpen = true;
    }

    public void Teleport(GameObject player)
    {
        if(player.transform.position.x < gameObject.transform.position.x)
            player.transform.position = new Vector3 (door.transform.position.x+1, door.transform.position.y, 0);
        else
            player.transform.position = new Vector3(door.transform.position.x - 1, door.transform.position.y, 0);
    }
}
