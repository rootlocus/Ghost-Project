using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Haunting : MonoBehaviour
{
    [Header("Room Sounds")]
    [SerializeField] AudioClip[] ghostSounds;
    [SerializeField] List<GameObject> markings;
    [SerializeField] GameObject demon;
    BoxCollider2D hauntingZone;
    AudioSource sfxPlayer;

    void Awake()
    {
        sfxPlayer = gameObject.GetComponent<AudioSource>();
        hauntingZone = gameObject.GetComponent<BoxCollider2D>();
        GameObject[] markings = GameObject.FindGameObjectsWithTag("Markings");
        foreach (GameObject marking in markings)
        {
            marking.SetActive(false);
        }
    }

    [Button("Prefill Demon")]
    public void GetDemon()
    {
        demon = GameObject.FindGameObjectWithTag("Enemy");
    }

    [Button("Play Clue Sound")]
    public void PlayRandomSound()
    {
        sfxPlayer.clip = ghostSounds[Random.Range(0, ghostSounds.Length)];
        sfxPlayer.Play();
    }

    [Button("Trigger Markings")]
    public void TriggerMarking()
    {
        if (markings.Count != 0)
        {
            markings[0].SetActive(true);
            markings.Remove(markings[0]);
        }
    }


    [Button("Spawn Ghost In Room")]
    public void SpawnGhost()
    {
        StartCoroutine("SpawnDemonInArea");
        //Play atmosphere
        // play ghost breathing
        //audioManager.Play("ClueFound");
        //get main demon from gamemanager
        // spawn in room
        //disappear after a few seconds

    }
    IEnumerator SpawnDemonInArea()
    {
        Vector2 hauntHere = RandomPointInBounds(hauntingZone.bounds);
        demon.transform.position = hauntHere;

        yield return new WaitForSeconds(5);
        demon.transform.position = new Vector2(1000f, 0f);
    }

    public static Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
