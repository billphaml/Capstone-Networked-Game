/******************************************************************************
 * The purpose of this class is to be a visual representation of the
 * playerActor equipment. It has a variable for the Inventory class in order to
 * instantiate it to each individual equipmentslot prefab under it in the
 * hierarchy. It checks for the EquipmentManager statEquipmentChanged in order
 * to update its slots visuals.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

#undef DEBUG

using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Inventory inventory;
    public EquipmentManager theEquipmentManager;

    public Transform itemParent;

    public GameObject inventoryUI;
    public PlayerStat thePlayer;
    public Canvas theCanvas;
    public EquipmentSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        theEquipmentManager.statEquipmentChanged += UpdateEquipment;

        itemSlot = itemParent.GetComponentsInChildren<EquipmentSlot>();

#if DEBUG
        Debug.Log("The equipment itemslot has " + itemSlot.Length);
#endif

        SetupManager();
    }

    private void UpdateEquipment()
    {
        if (thePlayer!= null)
        {
            UpdateEquipmentHandler();
        }
#if DEBUG
        Debug.Log("Time To Update The Inventory Gamer!");
#endif
    }

    public void UpdateEquipmentHandler()
    {
        if (thePlayer.playerHelmet != null)
        {
            itemSlot[0].AddItem(thePlayer.playerHelmet);
        }
        else
        {
            itemSlot[0].ClearSlot();
        }

        if (thePlayer.playerNecklace != null)
        {
            itemSlot[1].AddItem(thePlayer.playerNecklace);
        }
        else
        {
            itemSlot[1].ClearSlot();
        }

        if (thePlayer.playerArmor != null)
        {
            itemSlot[2].AddItem(thePlayer.playerArmor);
        }
        else
        {
            itemSlot[2].ClearSlot();
        }

        if (thePlayer.playerWeapon != null)
        {
            itemSlot[3].AddItem(thePlayer.playerWeapon);
        }
        else
        {
            itemSlot[3].ClearSlot();
        }

        if (thePlayer.playerRingOne != null)
        {
            itemSlot[4].AddItem(thePlayer.playerRingOne);
        }
        else
        {
            itemSlot[4].ClearSlot();
        }

        if (thePlayer.playerRingTwo != null)
        {
            itemSlot[5].AddItem(thePlayer.playerRingTwo);
        }
        else
        {
            itemSlot[5].ClearSlot();
        }
    }

    private void SetupManager()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].theInventory = inventory;
            itemSlot[i].theEquipmentManager = theEquipmentManager;
            itemSlot[i].theCanvas = transform.parent.parent.GetComponent<Canvas>();
        }
    }
}
