using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Movement playerMovement;

    void Awake()
    {
        if (!player) player = GameObject.Find("Player").GetComponent<Player>();
        if (!playerMovement) playerMovement = player.GetComponent<Movement>();
    }

    [Button("Player Death Event")]
    public void Execute()
    {
        player.Death();
        playerMovement.ToggleFreeze();
    }
}
