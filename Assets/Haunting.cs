using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haunting : MonoBehaviour
{
    [Header("Room Sounds")]
    [SerializeField] AudioClip[] ghostSounds;
    AudioSource sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        sfxPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        sfxPlayer.clip = ghostSounds[Random.Range(0, ghostSounds.Length)];
        sfxPlayer.Play();
    }
}
