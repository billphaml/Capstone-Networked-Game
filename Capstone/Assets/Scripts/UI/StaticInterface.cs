using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : PlayerUserInterface
{
    public GameObject[] EquipmentSlot;

    public override void createDisplay()
    {
        displayItem = new Dictionary<GameObject, InventorySlot>();
        for(int i  = 0; i < theInventory.storage.inventory.Length; i++)
        {
            var itemObject = EquipmentSlot[i];

            addEvent(itemObject, EventTriggerType.PointerEnter, delegate { mouseEnter(itemObject); });
            addEvent(itemObject, EventTriggerType.PointerExit, delegate { mouseExit(itemObject); });
            addEvent(itemObject, EventTriggerType.BeginDrag, delegate { mouseBeginDrag(itemObject); });
            addEvent(itemObject, EventTriggerType.EndDrag, delegate { mouseEndDrag(itemObject); });
            addEvent(itemObject, EventTriggerType.Drag, delegate { mouseDrag(itemObject); });

            displayItem.Add(itemObject, theInventory.storage.inventory[i]);
        }
    }

}
