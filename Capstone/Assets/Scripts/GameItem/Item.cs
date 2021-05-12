/******************************************************************************
 * Item class which uses values from gameItem scriptable object.
 *****************************************************************************/


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
