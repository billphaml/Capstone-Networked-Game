/******************************************************************************
 * Item variant for consumables.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "New Game Item", menuName = "GameItem/Items/Consumables")]
public class ConsumableItem : GameItem
{
    public int HPValue;
    public int SPValue;
    public int MPValue;

    void Awake()
    {
        gameItemType = itemType.CONSUME;   
    }
}
