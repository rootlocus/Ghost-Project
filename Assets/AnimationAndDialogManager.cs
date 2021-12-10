using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AnimationAndDialogManager : MonoBehaviour
{
    [SerializeField] public CompleteScreenUI completeUI;
    [SerializeField, BoxGroup("Managers")] DialogueManager dialogManager;
    [SerializeField, BoxGroup("Managers")] AudioManager audioManager;
    [SerializeField] DialogSO initialDialog;
    [SerializeField] DialogSO doorsLocked;

    void Awake()
    {
        if (!dialogManager) dialogManager = GameObject.Find("PlayerDialogCanvas").GetComponent<DialogueManager>();
        if (!completeUI) completeUI = GameObject.Find("CompletedCanvas").GetComponent<CompleteScreenUI>();
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void TriggerInitialDialog()
    {
        dialogManager.StartDialogue(initialDialog);
    }

    public void WalkToLockedEntrance()
    {
        dialogManager.StartDialogue(doorsLocked);
        audioManager.Play("GhostBreath_01");
    }

}
