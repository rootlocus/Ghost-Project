using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] AudioClip interactSFX;
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
        PlayInteractEffect();
    }

    private void PlayInteractEffect()
    {
        if (interactSFX)
        {
            audio.clip = interactSFX;
            audio.Play();
        }
    }

    public virtual void PlayDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
