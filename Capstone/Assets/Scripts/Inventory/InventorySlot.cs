 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public itemType[] allowedItem = new itemType[0];
    [System.NonSerialized]
    public PlayerUserInterface interfaceParent;
    public int ID = -1;
    public GameItem theItem;
    public int itemAmount;

    public InventorySlot(int iID, GameItem _theItem, int _itemAmount)
    {
        ID = iID;
        theItem = _theItem;
        itemAmount = _itemAmount;
    }

    public InventorySlot()
    {
        ID = -1;
        theItem = null;
        itemAmount = 0;
    }

    public void updateSlot(GameItem iItem, int iAmount)
    {
        ID = iItem.itemID;
        theItem = iItem;
        itemAmount = iAmount;
    }

    public void updateSlot(int iID, GameItem _theItem, int _itemAmount)
    {
        ID = iID;
        theItem = _theItem;
        itemAmount = _itemAmount;
    }

    public void addAmount(int iAmount)
    {
        itemAmount += iAmount;
    }

    public bool canPlace(GameItem iItem)
    {
        if(allowedItem.Length <= 0)
        {
            return true;
        }
        
        for(int i = 0; i < allowedItem.Length; i++)
        {
            if(iItem.gameItemType == allowedItem[i])
            {
                return true;
            }
        }

        return false;
    }
}
