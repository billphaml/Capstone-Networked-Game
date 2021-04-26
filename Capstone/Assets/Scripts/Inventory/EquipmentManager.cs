using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField] EquipItem[] currentEquipment;
    public InventoryUI theInventoryUserInterface;
    public PlayerStat thePlayerStat;

    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(itemType)).Length;

        currentEquipment = new EquipItem[numSlots];

    }

    public GameItem equipItem (EquipItem iEquipment)
    {
        switch (iEquipment.gameItemType)
        {
            case itemType.HEAD:
                return playerHelmetHandler(iEquipment);
                break;
            case itemType.ARMOR:
                return playerArmorHandler(iEquipment);
            case itemType.NECKLACE:
                return playerNecklaceHandler(iEquipment);
                break;
            case itemType.RING:
                return  ringSlotHandler(iEquipment);
                break;
            case itemType.SWORD:
                return playerWeaponhandler(iEquipment);
                break;
            case itemType.GREATSWORD:
                return playerWeaponhandler(iEquipment);
                break;
            case itemType.DAGGER:
                return playerWeaponhandler(iEquipment);
                break;
            case itemType.BOW:
                return playerWeaponhandler(iEquipment);
                break;
            case itemType.MAGIC:
                return playerWeaponhandler(iEquipment);
                break;
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

        return oldItem;
    }

    public void removeFromInventory(EquipItem iEquipment)
    {
        theInventoryUserInterface.inventory.removeItem(iEquipment);
    }


}
