using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public InventorySlot[] inventory;

    public Inventory(int arraySize)
    {
        inventory = new InventorySlot[arraySize];
    }

    public Inventory()
    {
        inventory = new InventorySlot[28];
    }


    public void Clear()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            inventory[i].updateSlot(-1, null, 0);
        }
    }
}
