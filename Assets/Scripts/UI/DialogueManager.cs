using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Text nameText = null;
    [SerializeField] Text dialogueText = null;
    [SerializeField] Queue<string> sentences = null;
    [SerializeField] Animator animator = null;
    [SerializeField] GameEvent OnDialogueStart = null;
    [SerializeField] GameEvent OnDialogueEnd = null;

    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        OnDialogueStart?.Raise();
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
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
    }

}
