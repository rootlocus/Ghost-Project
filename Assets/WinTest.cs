using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTest : Collidable
{
    [SerializeField] GameEvent OnLevelComplete;

    protected override void onCollide(Collider2D col) 
    {
        if(col.name == "Player") {
            OnLevelComplete.Raise();
            gameObject.SetActive(false);
        }
    }
}
