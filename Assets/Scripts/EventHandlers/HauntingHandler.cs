using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class HauntingHandler : MonoBehaviour
{
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] List<GameObject> roomsGO;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] List<Room> rooms;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int chosenHauntIndex = 0;
    [BoxGroup("Haunting Room"), Range(0, 10), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int hauntThreshold = 8;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int roomHauntDuration = 5;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] string ghostBreathing = "GhostBreath_01";
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] string ghostEntranceBGM = "PressureAtmos01";
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] Haunting chosenHauntingRoom;
    [SerializeField] Room chosenRoom;
    [BoxGroup("Haunt Objects")]
    [SerializeField] Zone zone;
    [SerializeField, BoxGroup("Haunting Objectives")] bool hasFoundMemorabilia = false;
    [SerializeField, BoxGroup("Haunting Objectives")] bool hasFoundName = false;
    [SerializeField, BoxGroup("Haunting Objectives")] bool hasExorcist = false;

    [SerializeField, BoxGroup("Managers")] AudioManager audioManager;

    [SerializeField, BoxGroup("Events")] GameEvent foundNameEvent;
    [SerializeField, BoxGroup("Events")] GameEvent OnLevelWin;
    [SerializeField, BoxGroup("Events")] GameEvent OnDamageTaken;
    [SerializeField, BoxGroup("Events")] GameEvent OnExorcist;

    void Awake()
    {
        if (!zone) zone = GameObject.FindGameObjectWithTag("Zone").GetComponent<Zone>(); // if no zone then skip
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        InitializeHauntingRooms();
        ChooseHauntingRoom();
    }

    void InitializeHauntingRooms()
    {
        GameObject[] hauntingGroupings = GameObject.FindGameObjectsWithTag("Haunting");

        foreach (GameObject haunt in hauntingGroupings)
        {
            roomsGO.Add(haunt);
            rooms.Add(haunt.GetComponent<Room>());
        }
    }

    IEnumerator DelayToWin()
    {
        yield return new WaitForSeconds(5f);

        OnLevelWin?.Raise();
    }

    void ChooseHauntingRoom()
    {
        chosenHauntIndex = Random.Range(0, roomsGO.Count - 1);
        chosenHauntingRoom = roomsGO[chosenHauntIndex].GetComponent<Haunting>();
        chosenHauntingRoom.SetChosenHauntingRoom();
        chosenRoom = rooms[chosenHauntIndex];

        //InvokeRepeating("ClueFoundTrigger", initTimeSound, Random.Range(minTimeClueSound, maxTimeClueSound));
    }

    [Button("Radio Check")]
    public void CheckRadioTrigger()
    {
        if (zone.IsInZone())
            TriggerNameFound();
        else
            CheckHauntThreshold();
    }

    public void FoundMemorabilia()
    {
        hasFoundMemorabilia = true;
    }

    public void FoundName()
    {
        hasFoundName = true;
    }

    public void AttemptExorcism()
    {
        if (PlayerInChosenHauntingRoom() && FoundRestOfObjectives())
        {
            hasExorcist = true;
            OnExorcist?.Raise();
            StartCoroutine(DelayToWin());
        }
        else
        {
            OnDamageTaken?.Raise();
        }
    }

    public void StartHauntingEffects()
    {
        audioManager.Play(ghostBreathing);
        audioManager.PlayBGM(ghostEntranceBGM);
    }

    public void StopHauntingEffects()
    {
        audioManager.PlayBGM("BGM_6");
    }

    bool PlayerInChosenHauntingRoom()
    {
        // get room player is in
        return rooms[chosenHauntIndex].IsPlayerInRoom();
    }

    bool FoundRestOfObjectives()
    {
        return hasFoundMemorabilia && hasFoundName;
    }

    void TriggerNameFound()
    {
        zone.DisableZone();
        audioManager.Play("ClueFound");
        foundNameEvent?.Raise();
    }

    void CheckHauntThreshold()
    {
        float hauntPlayerRoll = Random.Range(0, 10);

        if (hauntPlayerRoll >= hauntThreshold)
        {
            Haunting roomDirector = roomsGO.Find(room => room.GetComponent<Room>().IsPlayerInRoom()).GetComponent<Haunting>();
            roomDirector.EnableRoomAttack();
        }
    }
}
