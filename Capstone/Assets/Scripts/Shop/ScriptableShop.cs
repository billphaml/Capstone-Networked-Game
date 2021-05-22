using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Shop", menuName = "Shop/Store")]
public class ScriptableShop : ScriptableObject
{
    public string shopName;
    public List<ShopItem> Shopitem = new List<ShopItem>();
}
