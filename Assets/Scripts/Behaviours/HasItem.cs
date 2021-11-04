using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasItem : Interactable
{
    public List<GameObject> items;
    public Inventory inventory;

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    public void ObtainItem() {
        foreach (var item in items)
        {
            inventory.AddItem(item); // todo if inventory full then dont remove
            items.Remove(item);
        }
    }
}
