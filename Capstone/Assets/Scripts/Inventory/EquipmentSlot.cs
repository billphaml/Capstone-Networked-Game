using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;
    public Button dropButton;
    EquipItem theItem;
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
                clearSlot();
            }
        }
    }
}
