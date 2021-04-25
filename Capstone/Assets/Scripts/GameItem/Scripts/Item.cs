using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public string itemName;
    public int itemID;

    public Item (GameItem theItem)
    {
        itemName = theItem.itemName;
        itemID = theItem.itemID;
    }
}
