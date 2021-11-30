using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rosary : Item
{
    [SerializeField] GameEvent ExorcistGhost;

    public override void utilise()
    {
        ExorcistGhost?.Raise();
    }
}
