using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Interactable
{
    [Header("Furniture Clue")]
    [SerializeField] bool HasClue = false;
    [SerializeField] bool isChecked = false;
    [SerializeField] GameEvent OnClueFound;
    [SerializeField] GameEvent TriggerHauntRoll;

    public override void Interact()
    {
        if (!isChecked)
        {
            base.Interact();
            isChecked = true;
            FoundClue();
        }
    }

    void FoundClue()
    {
        if (HasClue)
        {
            HasClue = false;
            OnClueFound?.Raise();
        } else
        {
            TriggerHauntRoll.Raise();
        }
    }

    public void AddClue()
    {
        HasClue = true;
    }

    public bool GetIsChecked()
    {
        return isChecked;
    }
}
