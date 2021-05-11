/* This is the EquipmentUI class
 * The purpose of this class is to be a visual representation of the playerActor equipment.
 * It has a variable for the Inventory class in order to instantiate it to each individual equipmentslot prefab under it in the hierarchy 
 * It checks for the EquipmentManager statEquipmentChanged in order to update its slots visuals.
 * */

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
        theEquipmentManager.statEquipmentChanged += updateEquipment;

        itemSlot = itemParent.GetComponentsInChildren<EquipmentSlot>();

#if DEBUG
        Debug.Log("The equipment itemslot has " + itemSlot.Length);
#endif

        setupManager();
    }

    private void updateEquipment()
    {
        if (thePlayer!= null)
        {
            updateEquipmentHandler();
        }
#if DEBUG
        Debug.Log("Time To Update The Inventory Gamer!");
#endif
    }

    public void updateEquipmentHandler()
    {
        if (thePlayer.playerHelmet != null)
        {
            itemSlot[0].addItem(thePlayer.playerHelmet);
        }
        else
        {
            itemSlot[0].clearSlot();
        }

        if (thePlayer.playerNecklace != null)
        {
            itemSlot[1].addItem(thePlayer.playerNecklace);
        }
        else
        {
            itemSlot[1].clearSlot();
        }

        if (thePlayer.playerArmor != null)
        {
            itemSlot[2].addItem(thePlayer.playerArmor);
        }
        else
        {
            itemSlot[2].clearSlot();
        }

        if (thePlayer.playerWeapon != null)
        {
            itemSlot[3].addItem(thePlayer.playerWeapon);
        }
        else
        {
            itemSlot[3].clearSlot();
        }

        if (thePlayer.playerRingOne != null)
        {
            itemSlot[4].addItem(thePlayer.playerRingOne);
        }
        else
        {
            itemSlot[4].clearSlot();
        }

        if (thePlayer.playerRingTwo != null)
        {
            itemSlot[5].addItem(thePlayer.playerRingTwo);
        }
        else
        {
            itemSlot[5].clearSlot();
        }
    }

    private void setupManager()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].theInventory = inventory;
            itemSlot[i].theEquipmentManager = theEquipmentManager;
            itemSlot[i].theCanvas = transform.parent.parent.GetComponent<Canvas>();
        }
    }
}
