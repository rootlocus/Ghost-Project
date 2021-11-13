using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Interactable
{
    public bool hasClue = false;

    public override void Interact()
    {
        PlayDialogue();
    }
}
