using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    public Player thePlayer;

    public InventoryItem playerInventory;

    //private bool headCheck = false;
    //private bool armorCheck = false;
    //private bool weaponCheck = false;
    //private bool necklaceCheck = false;
    //private bool ringOneCheck = false;
    //private bool ringTwoCheck = false;

    void Awake()
    {
        setupPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerStat();
    }

    private void LateUpdate()
    {
        
    }

    // Method used to set up the player character
    public void setupPlayer()
    {
        thePlayer = new Player("PlayerName", "The Player", Actor.actorType.PLAYER, Actor.attackType.FIST);
        thePlayer.playerInventory = playerInventory;

    }

    public void itemChange()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Wow you hit something");
        var item = collision.GetComponent<ItemBehavior>();
        if (item)
        {
           thePlayer.playerInventory.addItem(item.theItem, 1);
            Destroy(collision.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        thePlayer.playerInventory.Inventory.Clear();
    }

    
    public void updateAttackType()
    {
        if(thePlayer.playerWeapon != null)
        {
            switch (thePlayer.playerWeapon.gameItemType)
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

        if(thePlayer.playerHelmet != null)
        {
            playerStrength += thePlayer.playerHelmet.addStrength;
            playerMagic += thePlayer.playerHelmet.addMagic;
            playerDexterity += thePlayer.playerHelmet.addDexterity;
            playerConstitution += thePlayer.playerHelmet.addConstitution;
            playerAttack += thePlayer.playerHelmet.addAttack;
            playerDefense += thePlayer.playerHelmet.addDefence;
            playerMagicResistance += thePlayer.playerHelmet.addMagicResistance;
            playerMovementSpeed += thePlayer.playerHelmet.addSpeed;
            playerCriticalChance += thePlayer.playerHelmet.addCriticalChance;
            playerCriticalDamage += thePlayer.playerHelmet.addCriticalDamage;
            playerResistance += thePlayer.playerHelmet.addResistance;
        }

        if(thePlayer.playerArmor != null)
        {
            playerStrength += thePlayer.playerArmor.addStrength;
            playerMagic += thePlayer.playerArmor.addMagic;
            playerDexterity += thePlayer.playerArmor.addDexterity;
            playerConstitution += thePlayer.playerArmor.addConstitution;
            playerAttack += thePlayer.playerArmor.addAttack;
            playerDefense += thePlayer.playerArmor.addDefence;
            playerMagicResistance += thePlayer.playerArmor.addMagicResistance;
            playerMovementSpeed += thePlayer.playerArmor.addSpeed;
            playerCriticalChance += thePlayer.playerArmor.addCriticalChance;
            playerCriticalDamage += thePlayer.playerArmor.addCriticalDamage;
            playerResistance += thePlayer.playerArmor.addResistance;
        }

        if (thePlayer.playerWeapon != null)
        {
            playerStrength += thePlayer.playerWeapon.addStrength;
            playerMagic += thePlayer.playerWeapon.addMagic;
            playerDexterity += thePlayer.playerWeapon.addDexterity;
            playerConstitution += thePlayer.playerWeapon.addConstitution;
            playerAttack += thePlayer.playerWeapon.addAttack;
            playerDefense += thePlayer.playerWeapon.addDefence;
            playerMagicResistance += thePlayer.playerWeapon.addMagicResistance;
            playerMovementSpeed += thePlayer.playerWeapon.addSpeed;
            playerCriticalChance += thePlayer.playerWeapon.addCriticalChance;
            playerCriticalDamage += thePlayer.playerWeapon.addCriticalDamage;
            playerResistance += thePlayer.playerWeapon.addResistance;
            updateAttackType();
        }
        else
        {
            updateAttackType();
        }

        if (thePlayer.playerNecklace != null)
        {
            playerStrength += thePlayer.playerNecklace.addStrength;
            playerMagic += thePlayer.playerNecklace.addMagic;
            playerDexterity += thePlayer.playerNecklace.addDexterity;
            playerConstitution += thePlayer.playerNecklace.addConstitution;
            playerAttack += thePlayer.playerNecklace.addAttack;
            playerDefense += thePlayer.playerNecklace.addDefence;
            playerMagicResistance += thePlayer.playerNecklace.addMagicResistance;
            playerMovementSpeed += thePlayer.playerNecklace.addSpeed;
            playerCriticalChance += thePlayer.playerNecklace.addCriticalChance;
            playerCriticalDamage += thePlayer.playerNecklace.addCriticalDamage;
            playerResistance += thePlayer.playerNecklace.addResistance;
        }

        if (thePlayer.playerRingOne != null)
        {
            playerStrength += thePlayer.playerRingOne.addStrength;
            playerMagic += thePlayer.playerRingOne.addMagic;
            playerDexterity += thePlayer.playerRingOne.addDexterity;
            playerConstitution += thePlayer.playerRingOne.addConstitution;
            playerAttack += thePlayer.playerRingOne.addAttack;
            playerDefense += thePlayer.playerRingOne.addDefence;
            playerMagicResistance += thePlayer.playerRingOne.addMagicResistance;
            playerMovementSpeed += thePlayer.playerRingOne.addSpeed;
            playerCriticalChance += thePlayer.playerRingOne.addCriticalChance;
            playerCriticalDamage += thePlayer.playerRingOne.addCriticalDamage;
            playerResistance += thePlayer.playerRingOne.addResistance;
        }

        if (thePlayer.playerRingTwo != null)
        {
            playerStrength += thePlayer.playerRingTwo.addStrength;
            playerMagic += thePlayer.playerRingTwo.addMagic;
            playerDexterity += thePlayer.playerRingTwo.addDexterity;
            playerConstitution += thePlayer.playerRingTwo.addConstitution;
            playerAttack += thePlayer.playerRingTwo.addAttack;
            playerDefense += thePlayer.playerRingTwo.addDefence;
            playerMagicResistance += thePlayer.playerRingTwo.addMagicResistance;
            playerMovementSpeed += thePlayer.playerRingTwo.addSpeed;
            playerCriticalChance += thePlayer.playerRingTwo.addCriticalChance;
            playerCriticalDamage += thePlayer.playerRingTwo.addCriticalDamage;
            playerResistance += thePlayer.playerRingTwo.addResistance;
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
        thePlayer.playerStrength += theItem.addStrength;
        thePlayer.playerMagic += theItem.addMagic;
        thePlayer.playerDexterity += theItem.addDexterity;
        thePlayer.playerConstitution += theItem.addConstitution;
        thePlayer.playerAttack += theItem.addAttack;
        thePlayer.playerDefense += theItem.addDefence;
        thePlayer.playerMagicResistance += theItem.addMagicResistance;
        thePlayer.playerMovementSpeed += theItem.addSpeed;
        thePlayer.playerCriticalChance += theItem.addCriticalChance;
        thePlayer.playerCriticalDamage += theItem.addCriticalDamage;
        thePlayer.playerResistance += theItem.addResistance;

        updateAttackType();
        calculatePlayerStat();
    }

    public void removeEquiptmentStat(GameItem theItem)
    {
        thePlayer.playerStrength -= theItem.addStrength;
        thePlayer.playerMagic -= theItem.addMagic;
        thePlayer.playerDexterity -= theItem.addDexterity;
        thePlayer.playerConstitution -= theItem.addConstitution;
        thePlayer.playerAttack -= theItem.addAttack;
        thePlayer.playerDefense -= theItem.addDefence;
        thePlayer.playerMagicResistance -= theItem.addMagicResistance;
        thePlayer.playerMovementSpeed -= theItem.addSpeed;
        thePlayer.playerCriticalChance -= theItem.addCriticalChance;
        thePlayer.playerCriticalDamage -= theItem.addCriticalDamage;
        thePlayer.playerResistance -= theItem.addResistance;

        updateAttackType();
        calculatePlayerStat();
    }
    **/
}
