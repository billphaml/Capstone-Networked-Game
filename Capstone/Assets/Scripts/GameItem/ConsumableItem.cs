using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Item", menuName = "GameItem/Items/Consumables")]
public class ConsumableItem : GameItem
{
    public int HPValue;
    public int SPValue;
    public int MPValue;

    void Awake()
    {
        itemGameType = itemType.CONSUME;   
    }
}
