/******************************************************************************
 * This class is implements the logic for the crafting system.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public List<RecipeScriptableObject> theItemRecipe = new List<RecipeScriptableObject>();

    public Transform itemParent;
    public Image resultScreen;
    public ItemSlot[] theCraftingSlot;

    // Start is called before the first frame update
    void Start()
    {
        theCraftingSlot = itemParent.GetComponentsInChildren<ItemSlot>();
    }

    public void UpdateRecipeResult()
    {
        if (theCraftingSlot[0].theItem == null || theCraftingSlot[1].theItem == null)
        {
            theCraftingSlot[2].ClearSlot();
            Debug.Log("Missing a component in the crafting slot");
        }
        else
        {
            for (int i = 0; i < theItemRecipe.Count; i++)
            {
                if (theCraftingSlot[0].theItem == theItemRecipe[i].item_01 && theCraftingSlot[1].theItem == theItemRecipe[i].item_02)
                {
                    theCraftingSlot[2].AddItem(theItemRecipe[i].output);
                    resultScreen.color = new Color(0.8207f,0.4293f,0);
                    return;
                } 
                else if (theCraftingSlot[0].theItem == theItemRecipe[i].item_02 && theCraftingSlot[1].theItem == theItemRecipe[i].item_01)
                {
                    theCraftingSlot[2].AddItem(theItemRecipe[i].output);
                    resultScreen.color = new Color(0.8207f, 0.4293f, 0);
                    return;
                }
            }
        }
    }

    public void ClaimResult()
    {
        if(theCraftingSlot[0].theItem != theCraftingSlot[2].returnItem)
        {
            theCraftingSlot[0].ClearSlot();
        }

        if (theCraftingSlot[1].theItem != theCraftingSlot[2].returnItem)
        {
            theCraftingSlot[1].ClearSlot();
        }

        theCraftingSlot[2].ClearSlot();
        resultScreen.color = new Color(0, 0, 0);
    }
}
