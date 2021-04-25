using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "GameItem/Inventory/Equipment")]
public class EquipmentItem : InventoryItem
{
     void Awake()
    {
        equipmentSetUp();
    }

    

    public void equipmentSetUp()
    {
        storage = new Inventory(6);
    }
}
