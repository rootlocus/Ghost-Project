using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HauntingHandler : MonoBehaviour
{
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] List<GameObject> rooms;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int chosenHauntIndex = 0;
    //[BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    //[SerializeField] string ghostName = "Anon";
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

    [BoxGroup("Managers")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] GameEvent foundNameEvent;
    //[SerializeField] List<GameObject> scares;
    //[SerializeField] Player player;

    void Awake()
    {
        if (!zone) zone = GameObject.FindGameObjectWithTag("Zone").GetComponent<Zone>(); // if no zone then skip
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        InitializeHauntingRooms();
        ChooseHauntingRoom();
    }

    IEnumerator CheckPlayerExitRoom(Room room)
    {
        while (room.IsPlayerInRoom())
        {
            yield return new WaitForSeconds(1f);
        }
        TransitionRoomHauntInactive();
    }

    void InitializeHauntingRooms()
    {
        GameObject[] hauntingGroupings = GameObject.FindGameObjectsWithTag("Haunting");

        foreach (GameObject haunt in hauntingGroupings)
        {
            rooms.Add(haunt);
        }
    }

    void ChooseHauntingRoom()
    {
        chosenHauntIndex = Random.Range(0, rooms.Count - 1);
        for (int i = 0; i < rooms.Count; i++)
        {
            if (chosenHauntIndex != i) rooms[i].SetActive(false);
        }
        chosenHauntingRoom = rooms[chosenHauntIndex].GetComponent<Haunting>();
        chosenRoom = rooms[chosenHauntIndex].GetComponent<Room>();
        //InvokeRepeating("ClueFoundTrigger", initTimeSound, Random.Range(minTimeClueSound, maxTimeClueSound));
    }

    [Button("Spawn Ghost Haunt")]
    public void TransitionRoomHaunt()
    {
        audioManager.Play(ghostBreathing);
        chosenHauntingRoom.EnableRoomAttack(); // next time can use room that player is in
        audioManager.PlayBGM(ghostEntranceBGM);
        StartCoroutine(CheckPlayerExitRoom(chosenRoom));
    }

    [Button("Despawn Ghost Haunt")]
    public void TransitionRoomHauntInactive()
    {
        chosenHauntingRoom.DisableRoomAttack();
        audioManager.PlayBGM("BGM_1");
    }

    //IEnumerator PlayGhostEnterBGM()
    //{
    //    audioManager.PlayBGM(ghostEntranceBGM);

    //    yield return new WaitForSeconds(roomHauntDuration);

    //    audioManager.PlayBGM("BGM_1");
    //}

    [Button("Radio Check")]
    public void CheckRadioTrigger()
    {
        if (zone.IsInZone())
            TriggerNameFound();
        else
            CheckHauntThreshold();

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
            TransitionRoomHaunt();
    }
}
