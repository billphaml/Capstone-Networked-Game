using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/RecipeScriptableObject")]
public class RecipeScriptableObject : ScriptableObject
{
    public ItemScriptableObject output;

    //3 by 3 matrix for crafting
    public ItemScriptableObject item_00;
    public ItemScriptableObject item_10;
    public ItemScriptableObject item_20;

    public ItemScriptableObject item_01;
    public ItemScriptableObject item_11;
    public ItemScriptableObject item_21;

    public ItemScriptableObject item_02;
    public ItemScriptableObject item_12;
    public ItemScriptableObject item_22;
}
