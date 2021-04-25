using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour, ISerializationCallbackReceiver
{
    public GameItem theItem;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = theItem.itemImage;
    }
}
