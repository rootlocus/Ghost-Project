using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Sprite door;
    public bool isOpen = false;
    // public bool isLock = false;

    public override void Interact()
    {
        toggleDoor();
    }

    private void toggleDoor()
    {
        isOpen = !isOpen;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = !isOpen;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = !isOpen;
    }
}
