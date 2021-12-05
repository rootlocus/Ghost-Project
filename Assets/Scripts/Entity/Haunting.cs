using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Haunting : MonoBehaviour
{
    [SerializeField, BoxGroup("Entities")] AudioClip[] roomSounds;
    [SerializeField, BoxGroup("Entities")] List<GameObject> markings;
    [SerializeField, BoxGroup("Entities")] GameObject demon;
    [SerializeField, BoxGroup("Entities")] Room room;

    //[SerializeField, BoxGroup("Haunting Mode")] bool isAttacking;
    [SerializeField, BoxGroup("Haunting Mode")] public RoomHaunt roomHaunt;

    [SerializeField, BoxGroup("Haunting Objectives")] bool hasFoundMemorabilia = false;
    [SerializeField, BoxGroup("Haunting Objectives")] bool hasFoundName = false;
    [SerializeField, BoxGroup("Haunting Objectives")] bool hasExorcist = false;

    [SerializeField] bool isChosenHauntingRoom;
    [SerializeField] GameEvent OnLevelWin;
    [SerializeField] GameEvent OnDamageTaken;
    PolygonCollider2D hauntingZone;
    AudioSource sfxPlayer;

    void Awake()
    {
        sfxPlayer = GetComponent<AudioSource>();
        hauntingZone = GetComponent<PolygonCollider2D>();
        room = GetComponent<Room>();
        roomHaunt = gameObject.GetComponentInChildren<RoomHaunt>();
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

    IEnumerator DelayToWin()
    {
        yield return new WaitForSeconds(5f);

        OnLevelWin?.Raise();
    }

    IEnumerator SpawnDemonInArea()
    {
        Vector2 hauntHere = RandomPointInBounds(hauntingZone.bounds);
        demon.transform.position = hauntHere;

        yield return new WaitForSeconds(5);
        demon.transform.position = new Vector2(1000f, 0f);
    }

    [Button("Prefill Demon")]
    public void GetDemon()
    {
        demon = GameObject.FindGameObjectWithTag("Enemy");
    }

    //[Button("Play Clue Sound")]
    //public void PlayRandomSound()
    //{
    //    sfxPlayer.clip = roomSounds[Random.Range(0, roomSounds.Length)];
    //    sfxPlayer.Play();
    //}

    [Button("Trigger Markings")]
    public void TriggerMarking()
    {
        if (markings.Count != 0)
        {
            markings[0].SetActive(true);
            markings.Remove(markings[0]);
        }
    }

    [Button("Attack Now")]
    public void EnableRoomAttack() // maybe in future can decide what kind of room puzzle attack
    {
        roomHaunt.gameObject.SetActive(true);
        roomHaunt.ActivateRoomAttack();
    }

    [Button("Stop Attacking")]
    public void DisableRoomAttack()
    {
        roomHaunt.gameObject.SetActive(false);
    }

    static Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }

    public void FoundMemorabilia()
    {
        hasFoundMemorabilia = true;
    }

    public void FoundName()
    {
        hasFoundName = true;
    }

    public void SetChosenHauntingRoom()
    {
        isChosenHauntingRoom = true;
    }

    public bool GetIsChosenHauntingRoom()
    {
        return isChosenHauntingRoom;
    }

    bool PlayerInChosenHauntingRoom()
    {
        return room.IsPlayerInRoom() && isChosenHauntingRoom;
    }

    bool FoundRestOfObjectives()
    {
        return hasFoundMemorabilia && hasFoundName;
    }

    public void OnExorcist()
    {
        if (PlayerInChosenHauntingRoom() && FoundRestOfObjectives())
        {
            hasExorcist = true;
            StartCoroutine(DelayToWin());
        } else
        {
            HurtPlayer();
        }
    }

    public void HurtPlayer()
    {
        //show other animations
        OnDamageTaken?.Raise();
    }
}
