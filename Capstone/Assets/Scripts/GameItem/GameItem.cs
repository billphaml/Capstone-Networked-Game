using System.Collections;
using System.Collections.Generic;
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

public abstract class GameItem : ScriptableObject
{

    // Item Description
    public GameObject itemGameObject;
    [Header("Game Item Description")]
    public string itemName;
    [TextArea(10,20)]
    public string itemDescription;
    public itemType gameItemType;

}
