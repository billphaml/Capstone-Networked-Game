/* This is the EquipmentManager Class
 * This class is used to facilitate the gameitem exchange from inventoryUI to EquipmentUI to Playerstat
 * It has an statEquipmentChanged in order to tell the playerStat to update the player's stats based on their equipped items
 **/

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
        if(thePlayerStat.thePlayer.playerHelmet != null)
        {
            oldItem = thePlayerStat.thePlayer.playerHelmet;
        }
        thePlayerStat.thePlayer.playerHelmet = iEquipment;

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
        if (thePlayerStat.thePlayer.playerNecklace != null)
        {
            oldItem = thePlayerStat.thePlayer.playerNecklace;
        }
        thePlayerStat.thePlayer.playerNecklace = iEquipment;

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
        if (thePlayerStat.thePlayer.playerArmor != null)
        {
            oldItem = thePlayerStat.thePlayer.playerArmor;
        }
        thePlayerStat.thePlayer.playerArmor = iEquipment;

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
        if (thePlayerStat.thePlayer.playerWeapon != null)
        {
            oldItem = thePlayerStat.thePlayer.playerWeapon;
        }
        thePlayerStat.thePlayer.playerWeapon = iEquipment;

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
        if(thePlayerStat.thePlayer.playerRingOne != null && thePlayerStat.thePlayer.playerRingTwo == null)
        {
            thePlayerStat.thePlayer.playerRingTwo = iEquipment;
        }
        else if (thePlayerStat.thePlayer.playerRingOne == null && thePlayerStat.thePlayer.playerRingTwo != null)
        {
            thePlayerStat.thePlayer.playerRingOne = iEquipment;
        }
        else
        {
            oldItem = thePlayerStat.thePlayer.playerRingOne;
            thePlayerStat.thePlayer.playerRingOne = iEquipment;
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
                thePlayerStat.thePlayer.playerHelmet = null;
                break;
            case 1:
                thePlayerStat.thePlayer.playerNecklace = null;
                break;
            case 2:
                thePlayerStat.thePlayer.playerArmor = null;
                break;
            case 3:
                thePlayerStat.thePlayer.playerWeapon = null;
                break;
            case 4:
                thePlayerStat.thePlayer.playerRingOne = null;
                break;
            case 5:
                thePlayerStat.thePlayer.playerRingTwo = null;
                break;
        }

    }
}
