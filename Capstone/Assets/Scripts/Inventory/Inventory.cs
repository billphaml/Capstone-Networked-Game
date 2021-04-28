using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using Photon.Realtime;
using UnityEngine;


/* This is the Inventory class
 * The purpose of this class is to act as the player's backend inventory. 
 * It contains a GameItem list and an inventory space int that acts as the maximum size of the inventory
 * It uses an OnItemChanged delegate in order to let other classes knows when an item is added to the inventory list or removed from it.
 **/ 
 
public class Inventory : MonoBehaviour
{

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public List<GameItem> inventoryItem = new List<GameItem>();
    public int inventorySpace = 28;

    public void addItem(GameItem iItem)
    {
        inventoryItem.Add(iItem);

        if(onItemChangedCallBack != null)
             onItemChangedCallBack.Invoke();
    }

    public void removeItem(GameItem iItem)
    {
        inventoryItem.Remove(iItem);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public bool canAdd()
    {
        if(inventoryItem.Count < inventorySpace)
        {
            return true;
        }

        return false;
    }
}
