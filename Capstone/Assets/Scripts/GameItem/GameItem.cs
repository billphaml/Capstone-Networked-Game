/******************************************************************************
 * Data values for items.
 *****************************************************************************/

using UnityEngine;

public enum itemType
{
    HEAD,
    ARMOR,
    NECKLACE,
    RING,
    SWORD,
    GREATSWORD,
    DAGGER,
    BOW,
    MAGIC,
    CONSUME,
    DEFAULT
}

[System.Serializable]
public abstract class GameItem : ScriptableObject
{
    // Item Description
    public int itemID;
    public Sprite itemImage;
    [Header("Game Item Description")]
    public string itemName;
    [TextArea(10,20)]
    public string itemDescription;
    public itemType gameItemType;

    public GameItem getThis()
    {
        return this;
    }
}
