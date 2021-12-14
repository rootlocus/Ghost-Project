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
    [SerializeField, BoxGroup("Entities")] Enemy enemy;
    [SerializeField, BoxGroup("Entities")] BoxCollider2D attackBoundary;
    [SerializeField, BoxGroup("Attack Config")] float maxDurationLook = 5f;
    [SerializeField, BoxGroup("Attack Config")] float maxDurationChill = 3f;
    [SerializeField, BoxGroup("Attack Config")] float attackTimeLeeway = 0.7f;
    [SerializeField, BoxGroup("Attack Config")] bool isLooking = false;
    [SerializeField, BoxGroup("Enemy Config")] float flickerDistance = 0.16f;
    [SerializeField, BoxGroup("Enemy Config")] float enemyChaseSpeed = 0.018f;
    bool chasePlayer = false;
    float currentDistanceToPlayer = 0f;

    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementV2>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        room = GetComponentInParent<Room>();
        roomDirector = GetComponentInParent<Haunting>();
        hauntingLight = GetComponent<Light2D>();
        attackBoundary = GameObject.Find("AttackBoundary").GetComponent<BoxCollider2D>();
    }

    void OnDisable()
    {
        isLooking = false;
        StopCoroutine(HauntingMode());
        StopCoroutine(FlickerLight());
        StopCoroutine(CheckPlayerSpotted());
        StopCoroutine(CheckPlayerExitRoom());
        HideEnemy();
    }

    private void FixedUpdate()
    {
        if (chasePlayer)
        {
            Vector3 fromPosition = enemy.transform.position;
            Vector3 toPosition = playerMovement.gameObject.transform.position;
            enemy.transform.position = Vector3.MoveTowards(fromPosition, toPosition, enemyChaseSpeed);
        }
    }

    static Vector2 RandomPointInBounds(Bounds bounds, float distanceToCenter)
    {
        float xPos;
        float yPos;

        if (Random.Range(0, 10) > 5)
        {
            xPos = bounds.min.x + distanceToCenter;
            yPos = bounds.min.y + distanceToCenter;
        }
        else
        {
            xPos = bounds.max.x - distanceToCenter;
            yPos = bounds.min.y + distanceToCenter;
        }

        return new Vector2(xPos, yPos);
    }

    void SpawnEnemyNearer()
    {
        Vector2 nearerPosition = RandomPointInBounds(attackBoundary.bounds, currentDistanceToPlayer);
        enemy.transform.position = nearerPosition;
        currentDistanceToPlayer += flickerDistance;
    }

    void HideEnemy()
    {
        enemy.transform.position = new Vector2(1000f, 0f);
    }

    IEnumerator CheckPlayerSpotted()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTimeLeeway);

            if (IsPlayerSpotted())
            {
                chasePlayer = true;
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

            SpawnEnemyNearer();

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
        chasePlayer = false;
        currentDistanceToPlayer = 0f;
        roomDirector.DisableRoomAttack();
        gameObject.SetActive(false);

        HideEnemy();
    }

}
