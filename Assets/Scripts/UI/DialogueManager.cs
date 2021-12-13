using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text nameText = null;
    [SerializeField] Text dialogueText = null;
    [SerializeField] Queue<string> newSentences = null;
    [SerializeField] Animator animator = null;
    [SerializeField] GameEvent OnDialogueStart = null;
    [SerializeField] GameEvent OnDialogueEnd = null;
    [SerializeField] bool dialogPlaying = false;
    [SerializeField] float speedOfText = 0.05f;

    void Awake() {
        newSentences = new Queue<string>();
        dialogBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && dialogPlaying)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (DialogSO dialog)
    {
        //if (newSentences.Count > 0)
        //    newSentences.Clear();
        dialogPlaying = true;
        OnDialogueStart?.Raise();
        dialogBox.SetActive(true);
        animator.SetBool("isOpen", true);
        InitializeDialogData(dialog);

        DisplayNextSentence();
    }

    private void InitializeDialogData(DialogSO dialog)
    {
        nameText.text = dialog.GetEntityName();
        foreach (string sentence in dialog.GetSentences())
        {
            newSentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (newSentences.Count == 0)
        {
            Debug.Log("ENMTER");
            EndDialogue();
            return;
        }

        string sentence = newSentences.Dequeue();
        dialogueText.text = sentence;
        
        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speedOfText);
            // yield return null; // this waits a single frame
        }
    }

    void EndDialogue()
    {
        dialogPlaying = false;
        OnDialogueEnd?.Raise();
        animator.SetBool("isOpen", false);
        newSentences.Clear();
        dialogBox.SetActive(false);
    }

}
