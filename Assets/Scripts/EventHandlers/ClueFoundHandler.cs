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
    [SerializeField, BoxGroup("Entity")] ObjectiveUI objectiveUI;
    [SerializeField, BoxGroup("Tutorial Config")] bool firstTime = false;
    [SerializeField, BoxGroup("Tutorial Config")] GameEvent firstTimeClue;
    [SerializeField, BoxGroup("Clues Config")] int maxMemorabiliaCount = 5;
    [SerializeField, BoxGroup("Scoreboard"), DisableInEditorMode] int foundMemorabiliaCount = 0;
    [SerializeField] GameEvent foundAllMemorabilia;


    [Button("Initialize Prefabs")]
    void Awake()
    {
        if (!player) player = GameObject.Find("Player").GetComponent<Player>();
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (!objectiveUI) objectiveUI = GameObject.Find("ObjectiveUI").GetComponent<ObjectiveUI>();
    }

    void Start()
    {
        InitializeHaunting();
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
        foundMemorabiliaCount++;
        if (foundMemorabiliaCount == maxMemorabiliaCount)
            foundAllMemorabilia?.Raise();
    }

    void UpdateUI()
    {
        objectiveUI.UpdateMemorabiliaCounter(foundMemorabiliaCount, maxMemorabiliaCount);
    }


    [Button("Clue Found Event")]
    public void Execute()
    {
        audioManager.Play("ClueFound");
        haunting.TriggerMarking();

        AddCounterToMemorabilia();
        UpdateUI();
        CheckFirstTimeTutorial();
    }

    public int GetMaxMemorabilia()
    {
        return maxMemorabiliaCount;
    }
}
