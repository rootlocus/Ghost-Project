using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Collidable
{
    [Header("Events")]
    [SerializeField] private GameEvent OnPlayerKill;

    protected override void onCollide(Collider2D col) 
    {
        if(col.name == "Player")
            OnPlayerKill.Raise();
    }


}
