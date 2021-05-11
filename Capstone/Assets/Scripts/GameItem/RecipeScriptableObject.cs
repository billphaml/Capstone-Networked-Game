using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Recipe", menuName = "GameItem/Items/ItemRecipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public GameItem output;

    //3 by 3 matrix for crafting
    public GameItem item_01;
    public GameItem item_02;
}
