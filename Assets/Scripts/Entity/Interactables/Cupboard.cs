using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : HasItem
{
    public bool isLock = false;

    public override void Interact()
    {
        if (isLock) {
            Debug.Log("ITS LOCKED");
        } else {
            Open();
        }
    }

    private void Open()
    {
        PlayDialogue();
        // ObtainItem();
    }

    private void unlock()
    {
        //if have key
        isLock = false;
    }
}
