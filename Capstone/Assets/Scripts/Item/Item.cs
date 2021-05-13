/******************************************************************************
 * Item class which uses values from gameItem scriptable object.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/


[System.Serializable]
public class Item 
{
    public string itemName;
    public int itemID;

    public Item(GameItem theItem)
    {
        itemName = theItem.itemName;
        itemID = theItem.itemID;
    }
}
