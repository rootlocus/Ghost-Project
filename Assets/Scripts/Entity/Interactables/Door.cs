using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : Interactable
{
    [SerializeField] bool isOpen = false;
    [SerializeField] BoxCollider2D doorCollider;
    // public bool isLock = false;

    void Awake()
    {
        player = gameObject.GetComponent<AudioSource>();
        doorCollider = GetComponentsInChildren<BoxCollider2D>().FirstOrDefault(x => x.gameObject != this.gameObject); ;
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            base.Interact();
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
