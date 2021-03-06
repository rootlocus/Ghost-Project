using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Haunting : MonoBehaviour
{
    [SerializeField, BoxGroup("Entities")] AudioClip[] roomSounds;
    [SerializeField, BoxGroup("Entities")] List<GameObject> markings;
    [SerializeField, BoxGroup("Entities")] GameObject enemy;
    [SerializeField, BoxGroup("Entities")] Room room;

    [SerializeField, BoxGroup("Initializations")] RoomHaunt roomHaunt;
    [SerializeField, BoxGroup("Initializations")] bool isChosenHauntingRoom;

    [SerializeField, BoxGroup("Events")] GameEvent OnHauntStart;
    [SerializeField, BoxGroup("Events")] GameEvent OnHauntEnd;
    PolygonCollider2D hauntingZone;
    AudioSource sfxPlayer;

    void Awake()
    {
        sfxPlayer = GetComponent<AudioSource>();
        hauntingZone = GetComponent<PolygonCollider2D>();
        room = GetComponent<Room>();
        roomHaunt = gameObject.GetComponentInChildren<RoomHaunt>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        roomHaunt.gameObject.SetActive(false);
        InitializeMarkings();
    }

    void InitializeMarkings()
    {
        GameObject[] markings = GameObject.FindGameObjectsWithTag("Markings");
        foreach (GameObject marking in markings)
        {
            marking.SetActive(false);
        }
    }

    IEnumerator SpawnDemonInArea()
    {
        Vector2 hauntHere = RandomPointInBounds(hauntingZone.bounds);
        enemy.transform.position = hauntHere;

        yield return new WaitForSeconds(5);
        enemy.transform.position = new Vector2(1000f, 0f);
    }

    [Button("Prefill Demon")]
    public void GetDemon()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
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

    [Button("Activate RG Attack")]
    public void EnableRoomAttack() // maybe in future can decide what kind of room puzzle attack
    {
        OnHauntStart?.Raise();
        roomHaunt.gameObject.SetActive(true);
        roomHaunt.ActivateRoomAttack();
    }

    [Button("Stop RG Attack")]
    public void DisableRoomAttack() 
    { 
        if (roomHaunt.gameObject.activeInHierarchy)
           roomHaunt.gameObject.SetActive(false);
        OnHauntEnd?.Raise();
    }

    static Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }

    public void SetChosenHauntingRoom()
    {
        isChosenHauntingRoom = true;
    }

    public bool GetIsChosenHauntingRoom()
    {
        return isChosenHauntingRoom;
    }

}
