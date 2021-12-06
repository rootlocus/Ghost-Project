using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] bool[] isFullArray;
    [SerializeField] GameObject[] slots;
    [SerializeField] Sprite defaultInventorySprite;
    [SerializeField] Sprite selectedInventorySprite;
    int selectedIndex = 0;

    void Update() 
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            selectedIndex = 0;
            ToggleItemUse(0);
        } 
        else if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            selectedIndex = 1;
            ToggleItemUse(1);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            selectedIndex = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            selectedIndex = 3;
        }

        if (selectedIndex != -1)
        {
            UseItem(selectedIndex);
            selectedIndex = -1;
        }
    }

    public void UseItem(int index)
    {
        try
        {
            slots[index].GetComponentInChildren<Item>().utilise();
        }
        catch (ArgumentOutOfRangeException outOfRange)
        {
            Debug.Log(outOfRange);
        }
    }

    public void ToggleItemUse(int index)
    {
        if (slots[index].GetComponent<Image>().sprite == selectedInventorySprite)
        {
            UnselectInventory(index);
        }
        else if (slots[index].GetComponent<Image>().sprite == defaultInventorySprite)
        {
            SelectInventory(index);
        }
    }

    public void SelectInventory(int slotIndex)
    {
        slots[slotIndex].GetComponent<Image>().sprite = selectedInventorySprite;
    }

    public void UnselectInventory(int slotIndex)
    {
        slots[slotIndex].GetComponent<Image>().sprite = defaultInventorySprite;
    }

    public void AddItem(GameObject gameItem)
    {
        int emptySlotIndex = Array.FindIndex(isFullArray, val => val.Equals(false));

        if (emptySlotIndex >= 0) {
            Instantiate(gameItem, this.slots[emptySlotIndex].gameObject.transform, false);
            isFullArray[emptySlotIndex] = true;
        }
    }

    //public void NextItem()
    //{
    //    // Reaching end of inventory
    //    if (selectedIndex == slots.Length - 1) {
    //        UnselectInventory(selectedIndex);
    //        selectedIndex = 0;
    //        SelectInventory(selectedIndex);
    //    } else {
    //        UnselectInventory(selectedIndex);
    //        selectedIndex++;
    //        SelectInventory(selectedIndex);
    //    }
    //}

    //public void PreviousItem()
    //{
    //    // Reaching start of inventory
    //    if (selectedIndex == 0) {
    //        UnselectInventory(selectedIndex);
    //        selectedIndex = slots.Length - 1;
    //        SelectInventory(selectedIndex);
    //    } else {
    //        UnselectInventory(selectedIndex);
    //        selectedIndex--;
    //        SelectInventory(selectedIndex);
    //    }
    //}

    public void UseSelectedItem()
    {
        try {
            slots[selectedIndex].GetComponentInChildren<Item>().utilise();
        } catch (ArgumentOutOfRangeException outOfRange) 
        {
            Debug.Log(outOfRange);
        }
    }
}
