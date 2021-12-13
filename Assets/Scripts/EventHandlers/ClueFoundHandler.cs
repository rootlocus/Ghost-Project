using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class ClueFoundHandler : MonoBehaviour
{
    [SerializeField, BoxGroup("Entity")] Player player;
    [SerializeField, BoxGroup("Entity")] AudioManager audioManager;
    [SerializeField, BoxGroup("Entity")] Haunting haunting;
    [SerializeField, BoxGroup("Tutorial Config")] bool firstTime = false;
    [SerializeField, BoxGroup("Tutorial Config")] GameEvent firstTimeClue;
    [SerializeField, BoxGroup("Clues Config")] int maxMemorabilia = 5;
    [SerializeField, BoxGroup("Scoreboard"), DisableInEditorMode] int foundMemorabilia = 0;
    [SerializeField] GameEvent foundAllMemorabilia;


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

    void CheckFirstTimeTutorial()
    {
        if (firstTime)
        {
            firstTimeClue?.Raise();
            firstTime = false;
        }
    }

    void AddCounterToMemorabilia()
    {
        foundMemorabilia++;
        if (foundMemorabilia == maxMemorabilia)
            foundAllMemorabilia?.Raise();
    }

    [Button("Clue Found Event")]
    public void Execute()
    {
        audioManager.Play("ClueFound");
        haunting.TriggerMarking();

        AddCounterToMemorabilia();

        CheckFirstTimeTutorial();
    }

}
