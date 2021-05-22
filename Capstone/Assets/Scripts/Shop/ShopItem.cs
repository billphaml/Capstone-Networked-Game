/******************************************************************************
 * The purpose of this class is to define a shopitem  
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public GameItem theShopItem;
    public int shopItemAmount;

    public ShopItem(GameItem theGameItem)
    {
        theShopItem = theGameItem;
        shopItemAmount = 1;
    }

    public ShopItem(GameItem theGameItem, int theItemAmount)
    {
        theShopItem = theGameItem;
        shopItemAmount = theItemAmount;
    }
}
