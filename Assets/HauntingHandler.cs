using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HauntingHandler : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] Haunting haunting;

    // Start is called before the first frame update
    void Start()
    {
        if (!haunting) haunting = GameObject.FindGameObjectWithTag("Haunting").GetComponent<Haunting>();
        if (!audioManager) audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    [Button("Spawn Ghost Scare")]
    public void SpawnGhost()
    {
        audioManager.Play("GhostBreath_01");
        haunting.SpawnGhost();
        StartCoroutine("PlayGhostEnterBGM"); //todo make fade in and out ?
    }

    IEnumerator PlayGhostEnterBGM()
    {
        audioManager.PlayBGM("PressureAtmos01");

        yield return new WaitForSeconds(5);

        audioManager.PlayBGM("BGM_1");
    }
}
