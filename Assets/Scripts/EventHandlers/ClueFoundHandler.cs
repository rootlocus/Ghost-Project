using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class ClueFoundHandler : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] AudioManager audioManager;
    [SerializeField] Haunting haunting;

    [Button("Initialize Prefabs")]
    void Awake()
    {
        InitializePlayer();
        InitializeAudioManager();
    }

    void Start()
    {
        InitializeHaunting();
    }

    void InitializeAudioManager()
    {
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void InitializePlayer()
    {
        if (!player) player = GameObject.Find("Player").GetComponent<Player>();
    }

    void InitializeHaunting()
    {
        if (!haunting) haunting = GameObject.FindGameObjectsWithTag("Haunting")
                .FirstOrDefault(room => room.GetComponent<Haunting>().GetIsChosenHauntingRoom())
                .GetComponent<Haunting>();
    }

    [Button("Clue Found Event")]
    public void Execute()
    {
        audioManager.Play("ClueFound");
        //haunting.PlayRandomSound();
        haunting.TriggerMarking();
    }

}
