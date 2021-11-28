using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : Interactable
{
    public Sprite door;
    public bool isOpen = false;
    public BoxCollider2D doorCollider;
    // public bool isLock = false;

    void Awake()
    {
        doorCollider = GetComponentsInChildren<BoxCollider2D>().FirstOrDefault(x => x.gameObject != this.gameObject); ;
    }
    public override void Interact()
    {
        if (!isOpen)
        {
            OpenDoor();
        } else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        doorCollider.enabled = false;
    }

    void CloseDoor()
    {
        isOpen = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        doorCollider.enabled = true;
    }
}
