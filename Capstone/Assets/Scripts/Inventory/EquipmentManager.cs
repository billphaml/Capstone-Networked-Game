/* This is the EquipmentManager Class
 * This class is used to facilitate the gameitem exchange from inventoryUI to EquipmentUI to Playerstat
 * It has an statEquipmentChanged in order to tell the playerStat to update the player's stats based on their equipped items
 **/

#undef DEBUG

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
                return playerHelmetHandler(iEquipment);
            case itemType.ARMOR:
                return playerArmorHandler(iEquipment);
            case itemType.NECKLACE:
                return playerNecklaceHandler(iEquipment);
            case itemType.RING:
                return ringSlotHandler(iEquipment);
            case itemType.SWORD:
                return playerWeaponhandler(iEquipment);
            case itemType.GREATSWORD:
                return playerWeaponhandler(iEquipment);
            case itemType.DAGGER:
                return playerWeaponhandler(iEquipment);
            case itemType.BOW:
                return playerWeaponhandler(iEquipment);
            case itemType.MAGIC:
                return playerWeaponhandler(iEquipment);
            default:
                return null;
        }
    }
    private GameItem playerHelmetHandler(EquipItem iEquipment)
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

    private GameItem playerNecklaceHandler(EquipItem iEquipment)
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

    private GameItem playerArmorHandler(EquipItem iEquipment)
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

    private GameItem playerWeaponhandler(EquipItem iEquipment)
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

    private GameItem ringSlotHandler(EquipItem iEquipment)
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

    public void removeFromInventory(EquipItem iEquipment)
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

        removeFromInventoryHandler(slotIndex);

        //if (onEquipmentChanged != null)
        //{
        //    onEquipmentChanged.Invoke(null, iEquipment);
        //}

        if (statEquipmentChanged != null)
        {
            statEquipmentChanged.Invoke();
        }
    }

    public void removeFromInventoryHandler(int iIndex)
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

    public int[] saveEquipped()
    {
        int[] playerEquipped = new int[6];

        for(int i = 0; i < playerEquipped.Length; i++)
        {
            EquipmentSlot equippedItem = currentEquipment[i];
            if(equippedItem.theItem != null)
            {
                playerEquipped[i] = equippedItem.theItem.itemID;
            }
            playerEquipped[i] = -1;
        }

        return playerEquipped;
    }

}
