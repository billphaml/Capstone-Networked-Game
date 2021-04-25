using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicInterface : PlayerUserInterface
{
    public GameObject inventoryPrefab;
    public int xStart;
    public int yStart;
    public int xBetweenItem;
    public int yBetweenItem;
    public int columnNum;

    public override void createDisplay()
    {
         displayItem = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < theInventory.storage.inventory.Length; i++)
        {
            var itemObject = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            itemObject.GetComponent<RectTransform>().localPosition = getPosition(i);

            if (theInventory.storage.inventory[i].theItem != null)
            {
                itemObject.transform.GetChild(0).GetComponent<Image>().sprite = theInventory.database.getItem[theInventory.storage.inventory[i].ID].itemImage;
            }

            addEvent(itemObject, EventTriggerType.PointerEnter, delegate { mouseEnter(itemObject); });
            addEvent(itemObject, EventTriggerType.PointerExit, delegate { mouseExit(itemObject); });
            addEvent(itemObject, EventTriggerType.BeginDrag, delegate { mouseBeginDrag(itemObject); });
            addEvent(itemObject, EventTriggerType.EndDrag, delegate { mouseEndDrag(itemObject); });
            addEvent(itemObject, EventTriggerType.Drag, delegate { mouseDrag(itemObject); });

            displayItem.Add(itemObject, theInventory.storage.inventory[i]);
        }
    }



    private Vector3 getPosition(int i)
    {
        return new Vector3(xStart + (xBetweenItem * (i % columnNum)), yStart + (-yBetweenItem * (i / columnNum)), 0f);
    }
}
