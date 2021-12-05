using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomHaunt : MonoBehaviour
{
    [SerializeField, BoxGroup("Entities")] MovementV2 playerMovement;
    [SerializeField, BoxGroup("Entities")] Room room;
    [SerializeField, BoxGroup("Entities")] Haunting roomDirector;
    [SerializeField, BoxGroup("Entities")] Light2D hauntingLight; 
    [SerializeField] bool isLooking = false;
    [SerializeField, BoxGroup("Attack Config")] float maxDurationLook = 5f;
    [SerializeField, BoxGroup("Attack Config")] float maxDurationChill = 3f;
    [SerializeField] GameEvent OnHauntStop;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementV2>();
        room = GetComponentInParent<Room>();
        roomDirector = GetComponentInParent<Haunting>();
        hauntingLight = GetComponent<Light2D>();
    }

    void OnDisable()
    {
        isLooking = false;
        StopCoroutine(HauntingMode());
        StopCoroutine(FlickerLight());
        StopCoroutine(CheckPlayerSpotted());
        StopCoroutine(CheckPlayerExitRoom());
    }

    IEnumerator CheckPlayerSpotted()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);

            if (IsPlayerSpotted())
            {
                roomDirector.HurtPlayer();
                OnHauntStop?.Raise();
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator FlickerLight()
    {
        // use InvokeRepeating ?
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            hauntingLight.intensity = Random.Range(0.8f, 1f);
        }
    }

    IEnumerator HauntingMode()
    {
        while (true)
        {
            isLooking = true;
            hauntingLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(2f, maxDurationLook));

            isLooking = false;
            hauntingLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(1f, maxDurationChill));
        }
    }

    IEnumerator CheckPlayerExitRoom()
    {
        while (room.IsPlayerInRoom())
        {
            yield return new WaitForSeconds(1f);
        }
        TransitionOutOfHaunt();
    }

    bool IsPlayerSpotted()
    {
        return playerMovement.CheckIfMoving() && isLooking && room.IsPlayerInRoom();
    }
    public void ActivateRoomAttack()
    {
        StartCoroutine(HauntingMode());
        StartCoroutine(FlickerLight());
        StartCoroutine(CheckPlayerSpotted());
        StartCoroutine(CheckPlayerExitRoom());
    }

    [Button("Despawn Ghost Haunt")]
    public void TransitionOutOfHaunt()
    {
        //audioManager.PlayBGM("BGM_1");
        gameObject.SetActive(false);
    }

    [Button("Spawn Ghost Haunt")]
    public void TransitionIntoHaunt()
    {
        if (room.IsPlayerInRoom())
        {
            //audioManager.Play(ghostBreathing);
            //audioManager.PlayBGM(ghostEntranceBGM);
            StartCoroutine(CheckPlayerExitRoom());
        }
    }
}
