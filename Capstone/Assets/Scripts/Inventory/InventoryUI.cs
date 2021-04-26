using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
