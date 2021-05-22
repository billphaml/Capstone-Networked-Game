using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    public string shopName;
    public List<ShopItem> theShopItem = new List<ShopItem>();

    public Shop(ScriptableShop theShopData)
    {
        shopName = theShopData.shopName;
        for(int i = 0; i < theShopData.Shopitem.Count; i++)
        {
            ShopItem addShopItem = new ShopItem(theShopData.Shopitem[i].theShopItem, theShopData.Shopitem[i].shopItemAmount);
            theShopItem.Add(addShopItem);
        }
    }

    public Shop(Shop theOtherShop)
    {
        shopName = theOtherShop.shopName;
        theShopItem = theOtherShop.theShopItem;
    }


}
