using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Item
{
    [SerializeField] GameEvent OnRadioUse;
    [SerializeField] AudioSource player;

    void Awake()
    {
        player = GetComponent<AudioSource>();
    }
    public override void utilise()
    {
        StartCoroutine("PlaySFX");
        OnRadioUse?.Raise();
    }

    IEnumerator PlaySFX()
    {
        player.Play();

        yield return new WaitForSeconds(.5f);

        player.Pause();
    }
}
