using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] bool InZone = false;
    [SerializeField] bool FoundZone = false;
    [SerializeField] GameEvent PlayerInZone;
    [SerializeField] GameEvent PlayerExitZone;
    [SerializeField] CircleCollider2D zoneCollider;

    private void Awake()
    {
        zoneCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InZone = true;
            PlayerInZone?.Raise();
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InZone = true;
            PlayerInZone?.Raise();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InZone = false;
            PlayerExitZone?.Raise();
        }
    }

    public bool IsInZone()
    {
        return InZone;
    }

    public void DisableZone()
    {
        FoundZone = true;
        zoneCollider.enabled = false;
    }
}
