using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] bool isPlayerInRoom;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isPlayerInRoom = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isPlayerInRoom = false;
    }

    public bool IsPlayerInRoom()
    {
        return isPlayerInRoom;
    }
}
