using System.Collections;
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

    public void updateRecipeResult()
    {
        if(theCraftingSlot[0].theItem == null || theCraftingSlot[1].theItem == null)
        {
            theCraftingSlot[2].clearSlot();
            Debug.Log("Missing a component in the crafting slot");
        }
        else
        {
            for(int i = 0; i < theItemRecipe.Count; i++)
            {
                if(theCraftingSlot[0].theItem == theItemRecipe[i].item_01 && theCraftingSlot[1].theItem == theItemRecipe[i].item_02)
                {
                    theCraftingSlot[2].addItem(theItemRecipe[i].output);
                    resultScreen.color = new Color(0.8207f,0.4293f,0);
                    return;
                } else if(theCraftingSlot[0].theItem == theItemRecipe[i].item_02 && theCraftingSlot[1].theItem == theItemRecipe[i].item_01)
                {
                    theCraftingSlot[2].addItem(theItemRecipe[i].output);
                    resultScreen.color = new Color(0.8207f, 0.4293f, 0);
                    return;
                }
            }
        }
    }

    public void claimResult()
    {
        if(theCraftingSlot[0].theItem != theCraftingSlot[2].returnItem)
        {
            theCraftingSlot[0].clearSlot();
        }

        if (theCraftingSlot[1].theItem != theCraftingSlot[2].returnItem)
        {
            theCraftingSlot[1].clearSlot();
        }

        theCraftingSlot[2].clearSlot();
        resultScreen.color = new Color(0, 0, 0);
    }
    

}
