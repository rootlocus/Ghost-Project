using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF : Item
{
    [SerializeField]
    private bool isActivated = false;

    public override void utilise()
    {
        isActivated = !isActivated;
        Debug.Log("EMF");
    }
}
