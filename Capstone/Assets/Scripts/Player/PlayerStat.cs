/******************************************************************************
 * This class contains the stats of the player and facilitates stat
 * manipulation.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

#undef DEBUG

using MLAPI;
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
            SetupPlayer();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            SetupInventory();
        }     
    }

    // Method used to set up the player character
    public void SetupPlayer()
    {
        thePlayer = new PlayerActor("PlayerName", "The Player", Actor.actorType.PLAYER, Actor.attackType.FIST);
        //playerInventory = playerInventory;

    }

    public void SetupInventory()
    {
        GameObject inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager");
        playerInventory = inventoryManager.GetComponent<Inventory>();
        inventoryManager.GetComponent<EquipmentManager>().thePlayerStat = this;
        inventoryManager.GetComponent<EquipmentManager>().statEquipmentChanged += UpdatePlayerStat;

        EquipmentUI playerEquipment;
        GameObject equipmentInterface = GameObject.FindGameObjectWithTag("Equipment UI");
        playerEquipment = equipmentInterface.GetComponent<EquipmentUI>();
        playerEquipment.thePlayer = this;
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
            if (item && playerInventory.CanAdd())
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
        playerInventory.AddItem(item.theItem);
        item.DestroyItemObjectServerRpc();
    }

    /// <summary>
    /// Add the passed in item to the player inventory. Only use if the object
    /// isn't spawned in the world and we're just adding a prefab.
    /// </summary>
    /// <param name="item"></param>
    public void AddItemPrefab(ItemBehavior item)
    {
        playerInventory.AddItem(item.theItem);
    }

    private void OnApplicationQuit()
    {
       // playerInventory.storage.inventory.Clear();
    }
    
    public void UpdateAttackType()
    {
        if(playerWeapon != null)
        {
            switch (playerWeapon.gameItemType)
            {
                case itemType.SWORD:
                   thePlayer.SetAttackType(Actor.attackType.SWORD);
                    break;
                case itemType.GREATSWORD:
                    thePlayer.SetAttackType(Actor.attackType.GREATSWORD);
                    break;
                case itemType.DAGGER:
                    thePlayer.SetAttackType(Actor.attackType.DAGGER);
                    break;
                case itemType.BOW:
                    thePlayer.SetAttackType(Actor.attackType.BOW);
                    break;
                case itemType.MAGIC:
                    thePlayer.SetAttackType(Actor.attackType.MAGIC);
                    break;
            }
        }
        else
        {
            thePlayer.SetAttackType(Actor.attackType.FIST);
        }
    }


    public void UpdatePlayerStat()
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

        if (playerHelmet != null)
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

        if (playerArmor != null)
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
            UpdateAttackType();
        }
        else
        {
            UpdateAttackType();
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

        thePlayer.playerStrength = playerStrength + thePlayer.GetStrength();
        thePlayer.playerMagic = playerMagic + thePlayer.GetMagic();
        thePlayer.playerDexterity = playerDexterity + thePlayer.GetDexterity();
        thePlayer.playerConstitution = playerConstitution + thePlayer.GetConstitution();
        thePlayer.playerAttack = playerAttack + thePlayer.GetAttack();
        thePlayer.playerDefense = playerDefense + thePlayer.GetDefense();
        thePlayer.playerMagicResistance = playerMagicResistance + thePlayer.GetMagicResistance();
        thePlayer.playerMovementSpeed = playerMovementSpeed + thePlayer.GetSpeed();
        thePlayer.playerCriticalChance = playerCriticalChance + thePlayer.GetCritChance();
        thePlayer.playerCriticalDamage = playerCriticalDamage + thePlayer.GetCritDamage();
        thePlayer.playerResistance = playerResistance + thePlayer.GetResistance();
    }

    public void AddHelmetStat()
    {

    }

    public void RemoveHelmetStat()
    {

    }

    public void AddArmorStat()
    {

    }

    public void RemoveArmorStat()
    {

    }

    public void AddWeaponStat()
    {

    }

    public void RemoveWeaponStat()
    {

    }

    public void AddNecklaceStat()
    {

    }

    public void RemoveNecklaceStat()
    {

    }

    public void AddRingOneStat()
    {

    }

    public void RemoveRingOneStat()
    {

    }

    public void AddRingTwoStat()
    {

    }

    public void RemoveRingTwoStat()
    {

    }

    public void CalculatePlayerStat()
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
