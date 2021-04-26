using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public EquipmentManager theEquipmentManager;

    public Inventory theInventory;
    public TextMeshProUGUI itemAmountText;
    public Button dropButton;
    GameItem theItem;
    int itemAmount = 0;

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
        transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
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