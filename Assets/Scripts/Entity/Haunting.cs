using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Haunting : MonoBehaviour
{
    [Header("Room Sounds")]
    [SerializeField] AudioClip[] ghostSounds;
    [SerializeField] List<GameObject> horrors;
    [SerializeField] GameObject demon;
    BoxCollider2D hauntingZone;
    AudioSource sfxPlayer;

    void Awake()
    {
        sfxPlayer = gameObject.GetComponent<AudioSource>();
        hauntingZone = gameObject.GetComponent<BoxCollider2D>();
    }

    [Button("Prefill Demon")]
    public void GetDemon()
    {
        demon = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void PlayRandomSound()
    {
        sfxPlayer.clip = ghostSounds[Random.Range(0, ghostSounds.Length)];
        sfxPlayer.Play();
    }

    [Button("Trigger Horror")]
    public void TriggerHorror()
    {
        if (horrors.Count != 0)
        {
            horrors[0].SetActive(true);
            horrors.Remove(horrors[0]);
        }
    }

    [Button("Trigger Scare")]
    public void TriggerScare()
    {
        StartCoroutine("DemonPresense");
        //get main demon from gamemanager
        // spawn in room
        //disappear after a few seconds

    }
    IEnumerator DemonPresense()
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
