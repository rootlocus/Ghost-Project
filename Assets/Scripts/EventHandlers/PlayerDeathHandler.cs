using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerDeathHandler : MonoBehaviour
{
    //[SerializeField] CompleteScreenUI ui;
    [SerializeField] Player player;

    void Awake()
    {
        //if (!ui) ui = GameObject.Find("CompletedCanvas").GetComponent<CompleteScreenUI>();
        if (!player) player = GameObject.Find("Player").GetComponent<Player>();
    }

    [Button("Player Death Event")]
    public void Execute()
    {
        player.Death();
        //ui.ToggleLoseScreen();
    }
}
