using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : Interactable
{
    [SerializeField] bool isOpen = false;
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] Animator animator;

    // public bool isLock = false;

    void Awake()
    {
        player = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        doorCollider = GetComponentsInChildren<BoxCollider2D>().FirstOrDefault(x => x.gameObject != this.gameObject); ;
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            base.Interact();
            OpenDoor();
        } 
    }

    void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("IsOpen", true);
        doorCollider.enabled = false;
    }

    void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("IsOpen", false);
        doorCollider.enabled = true;
    }
}
