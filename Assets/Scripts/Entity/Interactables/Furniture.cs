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
    [SerializeField] GameObject SearchBubble;
    [SerializeField] GameEvent searchFurniture;

    public override void Interact()
    {
        if (!isChecked)
        {

            StartCoroutine(StartSearching());
        }
    }

    IEnumerator StartSearching()
    {
        isChecked = true;
        searchFurniture?.Raise();
        PlaySearchAnimation(true);

        //freeze player movement here
        yield return new WaitForSeconds(2f);

        base.Interact();
        PlaySearchAnimation(false);
        SearchFurniture();
    }

    void SearchFurniture()
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

    public void PlaySearchAnimation(bool isSearching)
    {
        SearchBubble.SetActive(isSearching);
    }
}
