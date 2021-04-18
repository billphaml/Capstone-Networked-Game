using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public Player thePlayer;


    public GameItem[] playerInventory;

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
        
    }

    private void LateUpdate()
    {
        
    }

    // Method used to set up the player character
    public void setupPlayer()
    {
        thePlayer = new Player("PlayerName", "The Player", Actor.actorType.PLAYER, Actor.attackType.FIST);
        playerInventory = new GameItem[28];
    }

    public void itemChange()
    {
        
    }

    public void updateAttackType()
    {
        switch (thePlayer.playerWeapon.itemGameType)
        {
            case GameItem.itemType.GREATSWORD:
                thePlayer.setAttackType(Actor.attackType.GREATSWORD);
                break;
            case GameItem.itemType.SWORD:
                thePlayer.setAttackType(Actor.attackType.SWORD);
                break;
            case GameItem.itemType.DAGGER:
                thePlayer.setAttackType(Actor.attackType.DAGGER);
                break;
            case GameItem.itemType.BOW:
                thePlayer.setAttackType(Actor.attackType.BOW);
                break;
            case GameItem.itemType.MAGIC:
                thePlayer.setAttackType(Actor.attackType.MAGIC);
                break;
            default:
                thePlayer.setAttackType(Actor.attackType.FIST);
                break;
        }
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

}
