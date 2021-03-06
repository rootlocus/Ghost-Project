using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    AudioSource bgmPlayer;
    public static AudioManager instance;

    void Awake()
    {
        MaintainSingletonInstance();
        if (!bgmPlayer) bgmPlayer = GameObject.Find("BGM").GetComponent<AudioSource>();
        InitializeAudioSources(sounds);
    }

    private void MaintainSingletonInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void InitializeAudioSources(Sound[] clips)
    {
        foreach (Sound sound in clips)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play (string name)
    {
        Sound selectedSound = Array.Find(sounds, sound => sound.name == name);
        if (selectedSound == null)
        {
            Debug.LogWarning("Sound: " + selectedSound + " not found!");
            return;
        }
        selectedSound.source.Play();
    }

    public void PlayBGM (string name)
    {
        Sound selectedSound = Array.Find(sounds, sound => sound.name == name);
        if (selectedSound == null)
        {
            Debug.LogWarning("Sound: " + selectedSound + " not found!");
            return;
        }
        bgmPlayer.Stop();
        bgmPlayer.clip = selectedSound.clip;
        bgmPlayer.Play();
    }
}
