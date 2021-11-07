using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField]
    bool isActivated = false;

    void Start() {
        isConsumable = true;
    }

    public override void utilise()
    {
        isActivated = !isActivated;
        Debug.Log("Key");
    }
}
