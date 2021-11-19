using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] bool InZone = false;
    [SerializeField] bool FoundZone = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            InZone = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            InZone = false;
    }

    public bool IsInZone()
    {
        return InZone;
    }

    public void FoundTheZone()
    {
        FoundZone = false;
    }
}
