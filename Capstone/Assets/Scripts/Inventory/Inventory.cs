using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using Photon.Realtime;
using UnityEngine;

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
