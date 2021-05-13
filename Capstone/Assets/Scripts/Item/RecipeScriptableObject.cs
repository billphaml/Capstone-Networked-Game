/******************************************************************************
 * Scriptable object to store crafting recipes.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item Recipe", menuName = "GameItem/Items/ItemRecipe")]
public class RecipeScriptableObject : ScriptableObject
{
    public GameItem output;

    public GameItem item_01;
    public GameItem item_02;
}
