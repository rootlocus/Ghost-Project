using UnityEngine;

public class Enemy : Collidable
{
    [Header("Events")]
    [SerializeField] GameEvent OnPlayerKill;

    //protected override void onCollide(Collider2D col) 
    //{
    //    if(col.name == "Player")
    //        OnPlayerKill?.Raise();
    //}


}
