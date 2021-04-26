using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public List<EquipItem> inventoryItem = new List<EquipItem>();
    public int inventorySpace = 6;

    public void addItem(EquipItem iItem)
    {
        inventoryItem.Add(iItem);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public void removeItem(EquipItem iItem)
    {
        inventoryItem.Remove(iItem);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public bool canAdd()
    {
        if (inventoryItem.Count < inventorySpace)
        {
            return true;
        }

        return false;
    }
}
