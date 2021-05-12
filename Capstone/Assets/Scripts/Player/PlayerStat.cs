/******************************************************************************
 * This class contains the stats of the player and facilitates stat manipulation
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

#undef DEBUG

using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class PlayerStat : NetworkBehaviour
{
    public PlayerActor thePlayer;
    public Inventory playerInventory;


    [Header("Player Equipment")]
    public EquipItem playerHelmet;
    public EquipItem playerArmor;
    public EquipItem playerWeapon;
    public EquipItem playerNecklace;
    public EquipItem playerRingOne;
    public EquipItem playerRingTwo;

    void Awake()
    {
        if (IsLocalPlayer)
        {
            setupPlayer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            setupInventory();
        }     
    }

    // Method used to set up the player character
    public void setupPlayer()
    {
        thePlayer = new PlayerActor("PlayerName", "The Player", Actor.actorType.PLAYER, Actor.attackType.FIST);
        //playerInventory = playerInventory;

    }

    public void setupInventory()
    {
        GameObject inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager");
        playerInventory = inventoryManager.GetComponent<Inventory>();
        inventoryManager.GetComponent<EquipmentManager>().thePlayerStat = this;
        inventoryManager.GetComponent<EquipmentManager>().statEquipmentChanged += updatePlayerStat;

        EquipmentUI playerEquipment;
        GameObject equipmentInterface = GameObject.FindGameObjectWithTag("Equipment UI");
        playerEquipment = equipmentInterface.GetComponent<EquipmentUI>();
        playerEquipment.thePlayer = this;
    }

    public void itemChange()
    {
        
    }

    /// <summary>
    /// When colliding with an object checks if it is a item. If it is and the
    /// player has a free inventory slot then make a call to the server to try
    /// to pick it up.
    /// 
    /// Missing: check if collision is an item.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLocalPlayer)
        {
#if DEBUG
            Debug.Log("Wow you hit something");
#endif
            var item = collision.GetComponent<ItemBehavior>();

            // If object is an item and player has empty slot
            if (item && playerInventory.canAdd())
            {
                item.TryPickUpServerRpc(OwnerClientId);
            }
        }
    }

    /// <summary>
    /// Add the passed in item to player inventory and destroy the gameobject.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemBehavior item)
    {
        playerInventory.addItem(item.theItem);
        item.DestroyItemObjectServerRpc();
    }

    /// <summary>
    /// Add the passed in item to the player inventory. Only use if the object
    /// isn't spawned in the world and we're just adding a prefab.
    /// </summary>
    /// <param name="item"></param>
    public void AddItemPrefab(ItemBehavior item)
    {
        playerInventory.addItem(item.theItem);
    }

    private void OnApplicationQuit()
    {
       // playerInventory.storage.inventory.Clear();
    }
    
    public void updateAttackType()
    {
        if(playerWeapon != null)
        {
            switch (playerWeapon.gameItemType)
            {
                case itemType.SWORD:
                   thePlayer.setAttackType(Actor.attackType.SWORD);
                    break;
                case itemType.GREATSWORD:
                    thePlayer.setAttackType(Actor.attackType.GREATSWORD);
                    break;
                case itemType.DAGGER:
                    thePlayer.setAttackType(Actor.attackType.DAGGER);
                    break;
                case itemType.BOW:
                    thePlayer.setAttackType(Actor.attackType.BOW);
                    break;
                case itemType.MAGIC:
                    thePlayer.setAttackType(Actor.attackType.MAGIC);
                    break;
            }
        }
        else
        {
            thePlayer.setAttackType(Actor.attackType.FIST);
        }
    }


    public void updatePlayerStat()
    {
     int playerStrength = 0;
     int playerMagic = 0;
     int playerDexterity = 0;
     int playerConstitution = 0;
     int playerAttack = 0;
     int playerDefense = 0;
     int playerMagicResistance = 0;
     float playerMovementSpeed = 0;
     float playerCriticalChance = 0;
     float playerCriticalDamage = 0;
     float playerResistance = 0;
     float playerRange = 0;
     float playerSwingSpeed = 0;

        if(playerHelmet != null)
        {
            playerStrength += playerHelmet.addStrength;
            playerMagic += playerHelmet.addMagic;
            playerDexterity += playerHelmet.addDexterity;
            playerConstitution += playerHelmet.addConstitution;
            playerAttack += playerHelmet.addAttack;
            playerDefense += playerHelmet.addDefence;
            playerMagicResistance += playerHelmet.addMagicResistance;
            playerMovementSpeed += playerHelmet.addSpeed;
            playerCriticalChance += playerHelmet.addCriticalChance;
            playerCriticalDamage += playerHelmet.addCriticalDamage;
            playerResistance += playerHelmet.addResistance;
        }

        if(playerArmor != null)
        {
            playerStrength += playerArmor.addStrength;
            playerMagic += playerArmor.addMagic;
            playerDexterity += playerArmor.addDexterity;
            playerConstitution += playerArmor.addConstitution;
            playerAttack += playerArmor.addAttack;
            playerDefense += playerArmor.addDefence;
            playerMagicResistance += playerArmor.addMagicResistance;
            playerMovementSpeed += playerArmor.addSpeed;
            playerCriticalChance += playerArmor.addCriticalChance;
            playerCriticalDamage += playerArmor.addCriticalDamage;
            playerResistance += playerArmor.addResistance;
        }

        if (playerWeapon != null)
        {
            playerStrength += playerWeapon.addStrength;
            playerMagic += playerWeapon.addMagic;
            playerDexterity += playerWeapon.addDexterity;
            playerConstitution += playerWeapon.addConstitution;
            playerAttack += playerWeapon.addAttack;
            playerDefense += playerWeapon.addDefence;
            playerMagicResistance += playerWeapon.addMagicResistance;
            playerMovementSpeed += playerWeapon.addSpeed;
            playerCriticalChance += playerWeapon.addCriticalChance;
            playerCriticalDamage += playerWeapon.addCriticalDamage;
            playerResistance += playerWeapon.addResistance;
            updateAttackType();
        }
        else
        {
            updateAttackType();
        }

        if (playerNecklace != null)
        {
            playerStrength += playerNecklace.addStrength;
            playerMagic += playerNecklace.addMagic;
            playerDexterity += playerNecklace.addDexterity;
            playerConstitution += playerNecklace.addConstitution;
            playerAttack += playerNecklace.addAttack;
            playerDefense += playerNecklace.addDefence;
            playerMagicResistance += playerNecklace.addMagicResistance;
            playerMovementSpeed += playerNecklace.addSpeed;
            playerCriticalChance += playerNecklace.addCriticalChance;
            playerCriticalDamage += playerNecklace.addCriticalDamage;
            playerResistance += playerNecklace.addResistance;
        }

        if (playerRingOne != null)
        {
            playerStrength += playerRingOne.addStrength;
            playerMagic += playerRingOne.addMagic;
            playerDexterity += playerRingOne.addDexterity;
            playerConstitution += playerRingOne.addConstitution;
            playerAttack += playerRingOne.addAttack;
            playerDefense += playerRingOne.addDefence;
            playerMagicResistance += playerRingOne.addMagicResistance;
            playerMovementSpeed += playerRingOne.addSpeed;
            playerCriticalChance += playerRingOne.addCriticalChance;
            playerCriticalDamage += playerRingOne.addCriticalDamage;
            playerResistance += playerRingOne.addResistance;
        }

        if (playerRingTwo != null)
        {
            playerStrength += playerRingTwo.addStrength;
            playerMagic += playerRingTwo.addMagic;
            playerDexterity += playerRingTwo.addDexterity;
            playerConstitution += playerRingTwo.addConstitution;
            playerAttack += playerRingTwo.addAttack;
            playerDefense += playerRingTwo.addDefence;
            playerMagicResistance += playerRingTwo.addMagicResistance;
            playerMovementSpeed += playerRingTwo.addSpeed;
            playerCriticalChance += playerRingTwo.addCriticalChance;
            playerCriticalDamage += playerRingTwo.addCriticalDamage;
            playerResistance += playerRingTwo.addResistance;
        }

        thePlayer.playerStrength = playerStrength + thePlayer.getStrength();
        thePlayer.playerMagic = playerMagic + thePlayer.getMagic();
        thePlayer.playerDexterity = playerDexterity + thePlayer.getDexterity();
        thePlayer.playerConstitution = playerConstitution + thePlayer.getConstitution();
        thePlayer.playerAttack = playerAttack + thePlayer.getAttack();
        thePlayer.playerDefense = playerDefense + thePlayer.getDefense();
        thePlayer.playerMagicResistance = playerMagicResistance + thePlayer.getMagicResistance();
        thePlayer.playerMovementSpeed = playerMovementSpeed + thePlayer.getSpeed();
        thePlayer.playerCriticalChance = playerCriticalChance + thePlayer.getCritChance();
        thePlayer.playerCriticalDamage = playerCriticalDamage + thePlayer.getCritDamage();
        thePlayer.playerResistance = playerResistance + thePlayer.getResistance();
    }

    public void addHelmetStat()
    {

    }

    public void removeHelmetStat()
    {

    }

    public void addArmorStat()
    {

    }

    public void removeArmorStat()
    {

    }

    public void addWeaponStat()
    {

    }

    public void removeWeaponStat()
    {

    }

    public void addNecklaceStat()
    {

    }

    public void removeNecklaceStat()
    {

    }

    public void addRingOneStat()
    {

    }

    public void removeRingOneStat()
    {

    }

    public void addRingTwoStat()
    {

    }

    public void removeRingTwoStat()
    {

    }


    public void calculatePlayerStat()
    {

    }

    /*
    public void addEquiptmentStat(GameItem theItem)
    {
        playerStrength += theItem.addStrength;
        playerMagic += theItem.addMagic;
        playerDexterity += theItem.addDexterity;
        playerConstitution += theItem.addConstitution;
        playerAttack += theItem.addAttack;
        playerDefense += theItem.addDefence;
        playerMagicResistance += theItem.addMagicResistance;
        playerMovementSpeed += theItem.addSpeed;
        playerCriticalChance += theItem.addCriticalChance;
        playerCriticalDamage += theItem.addCriticalDamage;
        playerResistance += theItem.addResistance;

        updateAttackType();
        calculatePlayerStat();
    }

    public void removeEquiptmentStat(GameItem theItem)
    {
        playerStrength -= theItem.addStrength;
        playerMagic -= theItem.addMagic;
        playerDexterity -= theItem.addDexterity;
        playerConstitution -= theItem.addConstitution;
        playerAttack -= theItem.addAttack;
        playerDefense -= theItem.addDefence;
        playerMagicResistance -= theItem.addMagicResistance;
        playerMovementSpeed -= theItem.addSpeed;
        playerCriticalChance -= theItem.addCriticalChance;
        playerCriticalDamage -= theItem.addCriticalDamage;
        playerResistance -= theItem.addResistance;

        updateAttackType();
        calculatePlayerStat();
    }
    **/


}
