using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName = "GameItem/Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public List<InventorySlot> Inventory = new List<InventorySlot>();

    public void addItem(GameItem _theItem, int _itemAmount)
    {
        bool hasItem = false;

        for(int i = 0; i < Inventory.Count; i++)
        {
            if(Inventory[i].theItem == _theItem)
            {
                Inventory[i].addAmount(_itemAmount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Inventory.Add(new InventorySlot(_theItem, _itemAmount));
        }
    }

}

[System.Serializable]
public class InventorySlot
{
    public GameItem theItem;
    public int itemAmount;
    public InventorySlot(GameItem _theItem, int _itemAmount)
    {
        theItem = _theItem;
        itemAmount = _itemAmount;
    }

    public void addAmount(int iAmount)
    {
        itemAmount += iAmount;
    }
}
