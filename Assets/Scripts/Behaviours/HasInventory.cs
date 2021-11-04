using System;
using System.Collections.Generic;
using UnityEngine;

public class HasInventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField]
    // public int selectedIndex = 0;

    public void NextItem()
    {
        // selectedIndex = selectedIndex == items.Capacity ? 0 : selectedIndex++;
    }

    public void PreviousItem()
    {
        // selectedIndex = selectedIndex == 0 ? items.Capacity : selectedIndex--;
    }

    public void useSelectedItem()
    {
        try {
            // items[selectedIndex].utilise();
        } catch (ArgumentOutOfRangeException outOfRange) 
        {
            Debug.Log(outOfRange);
        }
    }
}
