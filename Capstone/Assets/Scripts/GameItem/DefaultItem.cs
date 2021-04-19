using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Item", menuName = "GameItem/Items/Default")]
public class DefaultItem : GameItem
{
    void Awake()
    {
        itemGameType = itemType.DEFAULT;
    }
}
