using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public bool isConsumable = false;

    public virtual void utilise() {
        Debug.Log("Nothing happened");
    }
}
