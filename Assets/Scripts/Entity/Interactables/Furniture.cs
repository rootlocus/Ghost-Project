using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Interactable
{
    [Header("Furniture Clue")]
    [SerializeField] bool HasClue = false;
    [SerializeField] GameEvent OnClueFound;

    public override void Interact()
    {
        base.Interact();
        FoundClue();
    }

    void FoundClue()
    {
        if (HasClue)
        {
            HasClue = false;
            OnClueFound?.Raise();
        }
    }

    public void AddClue()
    {
        HasClue = true;
    }
}
