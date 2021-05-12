/******************************************************************************
 * Generic item used for testing.
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
