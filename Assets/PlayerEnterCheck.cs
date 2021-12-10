using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCheck : MonoBehaviour
{
    [SerializeField] GameEvent OnPlayerEnter;
    [SerializeField] float delay = 0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            StartCoroutine(RaiseEvent());
    }

    IEnumerator RaiseEvent()
    {
        yield return new WaitForSeconds(delay);
        OnPlayerEnter?.Raise();
        gameObject.SetActive(false);
    }
}
