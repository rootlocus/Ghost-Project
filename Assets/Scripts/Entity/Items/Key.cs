using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField]
    private bool isActivated = false;

    private void Start() {
        Debug.Log("KEY");
        isConsumable = true;
    }

    public override void utilise()
    {
        isActivated = !isActivated;
        Debug.Log("Key");
    }
}
