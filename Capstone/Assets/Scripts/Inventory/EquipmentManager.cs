/******************************************************************************
 * This class is used to facilitate the gameitem exchange from inventoryUI to
 * EquipmentUI to Playerstat. It has an statEquipmentChanged in order to tell
 * the playerStat to update the player's stats based on their equipped items
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

#undef DEBUG

using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //public delegate void OnEquipmentChanged(EquipItem iItem, EquipItem oItem);
    //public OnEquipmentChanged onEquipmentChanged;

    public delegate void StatEquipmentChanged();
    public StatEquipmentChanged statEquipmentChanged;

    [SerializeField] EquipmentSlot[] currentEquipment;
    public InventoryUI theInventoryUserInterface;
    public PlayerStat thePlayerStat;

    private void Start()
    {
        EquipmentUI playerEquipment;
        GameObject equipmentInterface = GameObject.FindGameObjectWithTag("Equipment UI");
        playerEquipment = equipmentInterface.GetComponent<EquipmentUI>();

        currentEquipment = playerEquipment.itemSlot;
    }

    public GameItem equipItem (EquipItem iEquipment)
    {
        switch (iEquipment.gameItemType)
        {
            case itemType.HEAD:
                return PlayerHelmetHandler(iEquipment);
            case itemType.ARMOR:
                return PlayerArmorHandler(iEquipment);
            case itemType.NECKLACE:
                return PlayerNecklaceHandler(iEquipment);
            case itemType.RING:
                return RingSlotHandler(iEquipment);
            case itemType.SWORD:
                return PlayerWeaponhandler(iEquipment);
            case itemType.GREATSWORD:
                return PlayerWeaponhandler(iEquipment);
            case itemType.DAGGER:
                return PlayerWeaponhandler(iEquipment);
            case itemType.BOW:
                return PlayerWeaponhandler(iEquipment);
            case itemType.MAGIC:
                return PlayerWeaponhandler(iEquipment);
            default:
                return null;
        }
    }

    private GameItem PlayerHelmetHandler(EquipItem iEquipment)
    {
        GameItem oldItem = null;
        if(thePlayerStat.playerHelmet != null)
        {
            oldItem = thePlayerStat.playerHelmet;
        }
        thePlayerStat.playerHelmet = iEquipment;

        //if(onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(iEquipment, (EquipItem) oldItem);
        //}

        if(statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }

        return oldItem;
    }

    private GameItem PlayerNecklaceHandler(EquipItem iEquipment)
    {
        GameItem oldItem = null;
        if (thePlayerStat.playerNecklace != null)
        {
            oldItem = thePlayerStat.playerNecklace;
        }
        thePlayerStat.playerNecklace = iEquipment;

        //if (onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(iEquipment, (EquipItem)oldItem);
        //}

        if (statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }

        return oldItem;
    }

    private GameItem PlayerArmorHandler(EquipItem iEquipment)
    {
        GameItem oldItem = null;
        if (thePlayerStat.playerArmor != null)
        {
            oldItem = thePlayerStat.playerArmor;
        }
        thePlayerStat.playerArmor = iEquipment;

        //if (onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(iEquipment, (EquipItem)oldItem);
        //}

        if (statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }

        return oldItem;
    }

    private GameItem PlayerWeaponhandler(EquipItem iEquipment)
    {
        GameItem oldItem = null;
        if (thePlayerStat.playerWeapon != null)
        {
            oldItem = thePlayerStat.playerWeapon;
        }
        thePlayerStat.playerWeapon = iEquipment;

        //if (onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(iEquipment, (EquipItem)oldItem);
        //}

        if (statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }

        return oldItem;
    }

    private GameItem RingSlotHandler(EquipItem iEquipment)
    {
        GameItem oldItem = null;
        if(thePlayerStat.playerRingOne != null && thePlayerStat.playerRingTwo == null)
        {
            thePlayerStat.playerRingTwo = iEquipment;
        }
        else if (thePlayerStat.playerRingOne == null && thePlayerStat.playerRingTwo != null)
        {
            thePlayerStat.playerRingOne = iEquipment;
        }
        else
        {
            oldItem = thePlayerStat.playerRingOne;
            thePlayerStat.playerRingOne = iEquipment;
        }

        //if (onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(iEquipment, (EquipItem)oldItem);
        //}

        if (statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }

        return oldItem;
    }

    public void RemoveFromInventory(EquipItem iEquipment)
    {
        //theInventoryUserInterface.inventory.removeItem(iEquipment);
#if DEBUG
        Debug.Log("The equipment is " + iEquipment.itemName);
#endif
        int slotIndex = -1;

#if DEBUG
        Debug.Log("the length of the current equipment array is " + currentEquipment.Length);
#endif
        for (int i = 0; i < currentEquipment.Length; i++)
        {        
            if (currentEquipment[i].theItem == iEquipment)
            {
#if DEBUG
                Debug.Log("The current index is at " + i);
#endif
                slotIndex = i;
            }
        }

        RemoveFromInventoryHandler(slotIndex);

        //if (onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(null, iEquipment);
        //}

        if (statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }
    }

    public void RemoveFromInventoryHandler(int iIndex)
    {
        switch (iIndex)
        {
            case 0:
                thePlayerStat.playerHelmet = null;
                break;
            case 1:
                thePlayerStat.playerNecklace = null;
                break;
            case 2:
                thePlayerStat.playerArmor = null;
                break;
            case 3:
                thePlayerStat.playerWeapon = null;
                break;
            case 4:
                thePlayerStat.playerRingOne = null;
                break;
            case 5:
                thePlayerStat.playerRingTwo = null;
                break;
        }

    }

    public int[] SaveEquipped()
    {
        int[] playerEquipped = new int[6];

        for (int i = 0; i < playerEquipped.Length; i++)
        {
            EquipmentSlot equippedItem = currentEquipment[i];
            if (equippedItem.theItem != null)
            {
                playerEquipped[i] = equippedItem.theItem.itemID;
            }
            else
            {
                playerEquipped[i] = -1;
            }
        }

        return playerEquipped;
    }
}
