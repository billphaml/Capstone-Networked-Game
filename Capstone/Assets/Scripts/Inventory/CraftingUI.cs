using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform itemParent;
    public GameObject inventoryUI;
    public PlayerStat thePlayer;
    public Canvas theCanvas;
    public ItemSlot[] itemSlot;

    private void Start()
    {
        
    }

    private void setUpManager()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].theInventory = inventory;
            itemSlot[i].theCanvas = transform.parent.parent.GetComponent<Canvas>();
        }
    }
}
