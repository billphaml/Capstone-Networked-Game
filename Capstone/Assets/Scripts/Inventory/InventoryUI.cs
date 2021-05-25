/******************************************************************************
 * Inventory UI class 
 * This class is used to set up each individual inventory slot and update it
 * contents. When the class start, it set up the inventory connection and
 * equipment manager connection to each individual inventoryslot under it in
 * the hierarchy. It checks for the Inventory's onItemChangedCallBack delegate
 * to run updateInventory which updates the look and content of each individual
 * inventoryslot.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

#undef DEBUG

using MLAPI;
using UnityEngine;

public class InventoryUI : NetworkBehaviour
{
    public Inventory inventory;

    public EquipmentManager theEquipmentManager;

    public ShopManager theShopManager;

    public CraftingManager theCraftingManager;

    public PlayerHealable thePlayerHealable;

    public Transform itemParent;

    InventorySlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        inventory.onItemChangedCallBack += updateInventory;
        itemSlot = itemParent.GetComponentsInChildren<InventorySlot>();
        setupManager();
    }

    void Update()
    {
        if (thePlayerHealable == null)
        {
            try
            {
                thePlayerHealable = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.GetComponent<PlayerHealable>();
            } 
            catch
            {

            }
            
            if (thePlayerHealable != null) { setupNetworkManager(); }
        }
    }

    private void updateInventory()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (i < inventory.inventoryItem.Count)
            {
                itemSlot[i].AddItem(inventory.inventoryItem[i]);
            }
            else
            {
                itemSlot[i].ClearSlot();
            }
        }
#if DEBUG
        Debug.Log("Time To Update The Inventory Gamer!");
#endif
    }

    private void setupManager()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].theInventory = inventory;
            itemSlot[i].theEquipmentManager = theEquipmentManager;
            itemSlot[i].theCraftingManager = theCraftingManager;
            itemSlot[i].theShopManager = theShopManager;
            itemSlot[i].theCanvas = transform.parent.parent.GetComponent<Canvas>();
        }
    }

    private void setupNetworkManager()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].thePlayerHealable = thePlayerHealable;
        }
    }
}
