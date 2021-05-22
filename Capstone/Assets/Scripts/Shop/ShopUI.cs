using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{


    public Inventory inventory;
    public ShopManager theShopManager;
    public Transform itemParent;
    public TextMeshProUGUI shopTitleText;
    public TextMeshProUGUI stockRefillText;
    ShopSlot[] theShopSlot;

    private CanvasGroup shopUICanvasGroup;
    void Start()
    {
        theShopManager.onItemChangedCallBack += updateInventory;
        shopUICanvasGroup = GetComponent<CanvasGroup>();
        theShopSlot = itemParent.GetComponentsInChildren<ShopSlot>();
        setupManager();
    }

    private void Update()
    {
        stockRefillText.text = "Next Stock Refill " + (int)theShopManager.currentRestockTime;
    }

    private void updateInventory()
    {
        shopTitleText.text = theShopManager.activeShop.shopName;
     
        for (int i = 0; i < theShopSlot.Length; i++)
        {
            if (i < theShopManager.activeShop.theShopItem.Count)
            {
                theShopSlot[i].AddItem(theShopManager.activeShop.theShopItem[i].theShopItem);
                theShopSlot[i].itemAmount = theShopManager.activeShop.theShopItem[i].shopItemAmount;
                theShopSlot[i].itemAmountText.text = theShopManager.activeShop.theShopItem[i].shopItemAmount.ToString();
            }
            else
            {
                theShopSlot[i].ClearSlot();
            }
        }
#if DEBUG
        Debug.Log("Time To Update The Inventory Gamer!");
#endif
    }

    private void setupManager()
    {
        for (int i = 0; i < theShopSlot.Length; i++)
        {
            theShopSlot[i].theInventory = inventory;
            theShopSlot[i].theCanvas = transform.parent.GetComponent<Canvas>();
        }
    }

    public void turnOff()
    {
        UIManager.theUIManager.turnOffShop();
    }

    public void turnOn()
    {
        shopUICanvasGroup.alpha = 1;
        shopUICanvasGroup.interactable = true;
        shopUICanvasGroup.blocksRaycasts = true;
    }

}
