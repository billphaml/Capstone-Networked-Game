using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Shop", menuName = "Shop/ShopItem")]
public class ScriptableShopItem : ScriptableObject
{
    public GameItem theShopItem;
    public int shopItemAmount;
}
