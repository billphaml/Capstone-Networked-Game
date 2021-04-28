using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* This is the EquipmentSlot class
 * The purpose of this class is to represent the player's equipment in the equipment UI as a button.
 * It connects with the EquipmentManager in order to tell the EquipmentManager to take the item off the player when they're "clicked"
 * It connects to the Inventory in order to add its EquipItem into the inventory list when "clicked"
 * */
public class EquipmentSlot : MonoBehaviour
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;
    public Button dropButton;
    public EquipItem theItem;
    int itemAmount = 0;

    void Start()
    {
        
    }

    public void addItem(EquipItem iItem)
    {
        theItem = iItem;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = iItem.itemImage;
        //itemAmount++;
        //itemAmountText.text = itemAmount.ToString();
        dropButton.interactable = true;
    }

    public void clearSlot()
    {
        theItem = null;
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
        //itemAmount = 0;
        //itemAmountText.text = "";
        //dropButton.interactable = false;
    }

    public void onRemoveButton()
    {
        theInventory.removeItem(theItem);
    }

    public void UseItem()
    {
        if (theItem != null)
        {
            if (theInventory.canAdd())
            {
                GameItem returnItem = theItem;
                theInventory.addItem(returnItem);
                theEquipmentManager.removeFromInventory(theItem);
                clearSlot();
            }
        }
    }
}
