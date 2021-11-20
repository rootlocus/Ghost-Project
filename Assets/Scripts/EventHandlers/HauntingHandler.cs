using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HauntingHandler : MonoBehaviour
{
    [BoxGroup("Managers")]
    [SerializeField] AudioManager audioManager;
    [BoxGroup("Haunt Objects")]
    [SerializeField] Haunting haunting;
    [BoxGroup("Haunt Objects")]
    [SerializeField] Zone zone;
    [Range(0, 10)]
    [SerializeField] int hauntCheckChance = 8;
    [SerializeField] string ghostBreathing = "GhostBreath_01";
    [SerializeField] string ghostEntranceBGM = "PressureAtmos01";
    [SerializeField] int ghostScareDuration = 5;
    [SerializeField] GameEvent foundNameEvent;

    void Awake()
    {
        if (!zone) zone = GameObject.FindGameObjectWithTag("Zone").GetComponent<Zone>();
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        if (!haunting) haunting = GameObject.FindGameObjectWithTag("Haunting").GetComponent<Haunting>(); //todo make haunting not hidden
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
        else
        {
            float randomNumber = Random.Range(0, 10);

            if (randomNumber <= hauntCheckChance)
            {
                audioManager.Play("GhostAttack");
                SpawnGhost(); // spawn attack instead next time
            }
        }

    }
}
