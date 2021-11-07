using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTest : Collidable
{
    [SerializeField] GameEvent OnLevelWin;

    protected override void onCollide(Collider2D col) 
    {
        if(col.name == "Player") {
            OnLevelWin?.Raise();
            gameObject.SetActive(false);
        }
    }
}
