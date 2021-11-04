using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Item
{
    [SerializeField]
    private bool isActivated = false;

    public override void utilise()
    {
        isActivated = !isActivated;
        Debug.Log("Radio");
    }
}
