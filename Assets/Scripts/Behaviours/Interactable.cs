using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] AudioClip interactSFX;
    public AudioSource player;

    void Awake()
    {
        player = gameObject.GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
        PlayInteractEffect();
    }

    private void PlayInteractEffect()
    {
        if (interactSFX)
        {
            player.clip = interactSFX;
            player.Play();
        }
    }

    public virtual void PlayDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
