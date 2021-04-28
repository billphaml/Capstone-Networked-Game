using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Inventory UI class 
 * This class is used to set up each individual inventory slot and update it contents.
 * When the class start, it set up the inventory connection and equipment manager connection to each individual inventoryslot under it in the hierarchy
 * It check for the Inventory's onItemChangedCallBack delegate to run updateInventory which updates the look and content of each individual inventoryslot
 * */

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public EquipmentManager theEquipmentManager;

    public Transform itemParent;

    public GameObject inventoryUI;

    InventorySlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        inventory.onItemChangedCallBack += updateInventory;

        itemSlot = itemParent.GetComponentsInChildren<InventorySlot>();
        setupManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            inventoryUI.SetActive(true);
        }

        if (Input.GetKey(KeyCode.M))
        {
            inventoryUI.SetActive(false);
        }
    }

    private void updateInventory()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if(i < inventory.inventoryItem.Count)
            {
                itemSlot[i].addItem(inventory.inventoryItem[i]);
            }
            else
            {
                itemSlot[i].clearSlot();
            }
        }
        Debug.Log("Time To Update The Inventory Gamer!");
    }

    private void setupManager()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].theInventory = inventory;
            itemSlot[i].theEquipmentManager = theEquipmentManager;
        }

       
    }
}
