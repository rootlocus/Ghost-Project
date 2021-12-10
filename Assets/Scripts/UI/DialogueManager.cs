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

    void Awake() {
        newSentences = new Queue<string>();
    }

    public void StartDialogue (DialogSO dialog)
    {
        dialogBox.SetActive(true);

        OnDialogueStart?.Raise();
        animator.SetBool("isOpen", true);
        nameText.text = dialog.GetEntityName();
        if (newSentences.Count > 0)
            newSentences.Clear();

        foreach (string sentence in dialog.GetSentences())
        {
            newSentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (newSentences.Count == 0)
        {
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
            yield return new WaitForSeconds(.05f);
            // yield return null; // this waits a single frame
        }
    }

    void EndDialogue()
    {
        OnDialogueEnd?.Raise();
        animator.SetBool("isOpen", false);
        dialogBox.SetActive(false);
    }

}
