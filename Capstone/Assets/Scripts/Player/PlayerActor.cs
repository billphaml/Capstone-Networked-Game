using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerActor : Actor
{

    [Header("Player Equipment")]
    public EquipItem playerHelmet;
    public EquipItem playerArmor;
    public EquipItem playerWeapon;
    public EquipItem playerNecklace;
    public EquipItem playerRingOne;
    public EquipItem playerRingTwo;

    [Header("Player Final Stats")]
    public int playerStrength = 10;
    public int playerMagic = 10;
    public int playerDexterity = 10;
    public int playerConstitution = 10;
    public int playerAttack = 10;
    public int playerDefense = 10;
    public int playerMagicResistance = 10;
    public float playerMovementSpeed = 4.5f;
    public float playerCriticalChance = 0.15f;
    public float playerCriticalDamage = 1f;
    public float playerResistance = 0.2f;
    public float playerRange = 0;
    public float playerSwingSpeed = 0;


    public PlayerActor(string name, string description, actorType theType, attackType theAttack) : base(name, description, theType, theAttack)
    {

    }

    public PlayerActor(PlayerActor iPlayer) : base(iPlayer.actorName, iPlayer.actorDescription, iPlayer.getActorType(), iPlayer.getAttackType())
    {

    }

}
