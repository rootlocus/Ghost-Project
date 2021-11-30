using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Item
{
    [SerializeField] GameEvent OnRadioUse;
    [SerializeField] AudioSource audioPlayer;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public override void utilise()
    {
        StartCoroutine("PlaySFX");
        OnRadioUse?.Raise();
    }

    IEnumerator PlaySFX()
    {
        audioPlayer.Play();

        yield return new WaitForSeconds(.5f);

        audioPlayer.Pause();
    }
}
