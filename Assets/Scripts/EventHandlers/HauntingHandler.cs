using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HauntingHandler : MonoBehaviour
{
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] List<GameObject> possibleHauntings;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int chosenHaunt = 0;
    //[BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    //[SerializeField] string ghostName = "Anon";
    //[BoxGroup("Haunting Room"), Range(0, 10), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    //[SerializeField] int hauntCheckChance = 8;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] int ghostScareDuration = 5;
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] string ghostBreathing = "GhostBreath_01";
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] string ghostEntranceBGM = "PressureAtmos01";
    [BoxGroup("Haunting Room"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [SerializeField] Haunting haunting;
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

    void InitializeHauntingRooms()
    {
        GameObject[] hauntingGroupings = GameObject.FindGameObjectsWithTag("Haunting");

        foreach (GameObject haunt in hauntingGroupings)
        {
            possibleHauntings.Add(haunt);
        }
    }

    void ChooseHauntingRoom()
    {
        chosenHaunt = Random.Range(0, possibleHauntings.Count - 1);
        for (int i = 0; i < possibleHauntings.Count; i++)
        {
            if (chosenHaunt != i) possibleHauntings[i].SetActive(false);
        }
        haunting = possibleHauntings[chosenHaunt].GetComponent<Haunting>();
        //InvokeRepeating("ClueFoundTrigger", initTimeSound, Random.Range(minTimeClueSound, maxTimeClueSound));
    }

    [Button("Spawn Ghost Scare")]
    public void SpawnGhost()
    {
        audioManager.Play(ghostBreathing);
        haunting.SpawnGhost();
        StartCoroutine("PlayGhostEnterBGM"); //todo make fade in and out ?
    }

    IEnumerator PlayGhostEnterBGM()
    {
        audioManager.PlayBGM(ghostEntranceBGM);

        yield return new WaitForSeconds(ghostScareDuration);

        audioManager.PlayBGM("BGM_1");
    }

    [Button("Radio Check")]
    public void CheckRadioTrigger()
    {
        if (zone.IsInZone())
        {
            zone.FoundTheZone();
            audioManager.Play("ClueFound");
            foundNameEvent?.Raise();
        }
        //else
        //{
        //    float randomNumber = Random.Range(0, 10);

        //    if (randomNumber <= hauntCheckChance)
        //    {
        //        audioManager.Play("GhostAttack");

        //        Scare();
        //        //SpawnGhost(); // spawn attack instead next time
        //    }
        //}

    }

    //[Button("Scare Player")]
    //public void Scare()
    //{
    //    Instantiate(scares[0], player.transform);
    //    scares[0].SetActive(true);
    //}
}
