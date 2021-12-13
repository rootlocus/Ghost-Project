using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCheck : MonoBehaviour
{
    [SerializeField] GameEvent OnPlayerEnter;
    [SerializeField] GameObject[] disableSelectedObjects;
    [SerializeField] GameObject[] enableSelectedObjects;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float delay = 0f;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            StartCoroutine(RaiseEvent());
    }

    void DisableSelectedObjects()
    {
        foreach (GameObject selectedObject in disableSelectedObjects)
        {
            selectedObject.SetActive(false);
        }
    }

    void EnableSelectedObjects()
    {
        foreach (GameObject selectedObject in enableSelectedObjects)
        {
            selectedObject.SetActive(true);
        }
    }

    IEnumerator RaiseEvent()
    {
        yield return new WaitForSeconds(delay);
        OnPlayerEnter?.Raise();
        boxCollider.enabled = false;
        DisableSelectedObjects();
        EnableSelectedObjects();
    }
}
