using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* This is the InventorySlot class
 * The purpose of this class is to represent each individual "box" in a player's inventory
 * Each one of the InventorySlot is a button; when clicked, it called the UseItem method to differentiate each individual item's function based on type
 * It contact with the EquipmentManager in order to allows for equipment communication
 * 
 * Stores the Item value and maybe the item amount if we do item stacking
 * 
 * */

public class InventorySlot : MonoBehaviour
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;
    public Button dropButton;
    GameItem theItem;
    int itemAmount = 0;

    /// <summary>
    /// Background image for empty item slot.
    /// </summary>
    [SerializeField] private Sprite emptyImage;

     void Start()
    {
    }

    public void addItem(GameItem iItem)
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
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptyImage;
        itemAmount = 0;
        itemAmountText.text = "";
        dropButton.interactable = false;
    }

    public void onRemoveButton()
    {
        theInventory.removeItem(theItem);
    }

    public void UseItem()
    {
        if(theItem != null)
        {
            //theItem.useItem();
            switch (theItem.gameItemType)
            {
                case itemType.CONSUME:
                    break;
                case itemType.DEFAULT:
                    break;
                default:
                    EquipItem dummyEquip = (EquipItem)theItem;
                    GameItem returnItem = theEquipmentManager.equipItem(dummyEquip);

                    if(returnItem == null)
                    {
                        theInventory.removeItem(theItem);
                    }
                    else
                    {
                        theInventory.removeItem(theItem);
                        theInventory.addItem(returnItem);
                    }
                    break;
            }
        }
    }
}