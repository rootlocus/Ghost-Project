using UnityEngine;

public class Enemy : Collidable
{
    [Header("Events")]
    [SerializeField] GameEvent OnPlayerTakeDamage;

    protected override void onCollide(Collider2D col)
    {
        if (col.name == "Player")
            OnPlayerTakeDamage?.Raise();
    }

}
