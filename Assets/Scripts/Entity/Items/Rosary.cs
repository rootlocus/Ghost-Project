using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosary : Item
{
    [SerializeField] GameEvent AttemptExorcist;

    public override void utilise()
    {
        AttemptExorcist?.Raise();
    }
}
