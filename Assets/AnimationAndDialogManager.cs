using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimationAndDialogManager : MonoBehaviour
{
    [SerializeField] CompleteScreenUI completeUI;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField, BoxGroup("Managers")] DialogueManager dialogManager;
    [SerializeField, BoxGroup("Managers")] AudioManager audioManager;
    [SerializeField, BoxGroup("Dialogs")] DialogSO initialDialog;
    [SerializeField, BoxGroup("Dialogs")] DialogSO doorsLocked;
    [SerializeField, BoxGroup("Dialogs")] DialogSO dangerousToGoAlone;
    [SerializeField, BoxGroup("Dialogs")] DialogSO inventoryTutorial;
    [SerializeField, BoxGroup("Dialogs")] DialogSO firstTimeClue;
    [SerializeField, BoxGroup("Dialogs")] DialogSO hauntingTutorial;
    [SerializeField] GameObject ghostHunter;

    void Awake()
    {
        if (!dialogManager) dialogManager = GameObject.Find("PlayerDialogCanvas").GetComponent<DialogueManager>();
        if (!completeUI) completeUI = GameObject.Find("CompletedCanvas").GetComponent<CompleteScreenUI>();
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!inventoryUI) inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryUI>();
    }

    void Start()
    {
        inventoryUI.gameObject.SetActive(false);
    }

    public void TriggerInitialDialog()
    {
        dialogManager.StartDialogue(initialDialog);
    }

    public void WalkToLockedEntrance()
    {
        dialogManager.StartDialogue(doorsLocked);
        audioManager.Play("GhostBreath_01");
        ghostHunter.SetActive(true);
    }

    public void FoundGhostHunter()
    {
        dialogManager.StartDialogue(dangerousToGoAlone);
    }

    public void InventoryTutorial()
    {
        dialogManager.StartDialogue(inventoryTutorial);
        inventoryUI.gameObject.SetActive(true);
    }

    public void FirstTimeClueFoundTutorial()
    {
        dialogManager.StartDialogue(firstTimeClue);
    }

    public void HauntingTutorial()
    {
        dialogManager.StartDialogue(hauntingTutorial);
    }
}
