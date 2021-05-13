/******************************************************************************
 * Generic item used for testing.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "New Game Item", menuName = "GameItem/Items/Default")]
public class DefaultItem : GameItem
{
    void Awake()
    {
        gameItemType = itemType.DEFAULT;
    }
}
