/******************************************************************************
 * This Class is derived from actor and contains the stats of the player as 
 * well as the inventory and equipped items for when the player saves. It can
 * be read in to load the player data.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;

[System.Serializable]
public class PlayerActor : Actor
{
    [Header("Player Final Stats")]
    public int playerStrength = 10;
    public int playerMagic = 10;
    public int playerDexterity = 10;
    public int playerConstitution = 10;
    public int playerAttack = 10;
    public int playerDefense = 10;
    public int playerMagicResistance = 10;
    public int gold = 0;
    public float playerMovementSpeed = 4.5f;
    public float playerCriticalChance = 0.15f;
    public float playerCriticalDamage = 1f;
    public float playerResistance = 0.2f;
    public float playerRange = 0;
    public float playerSwingSpeed = 0;

    public int[] inventory;
    public int[] equipment;

    public PlayerActor(string name, string description, actorType theType, attackType theAttack) : base(name, description, theType, theAttack)
    {

    }

    public PlayerActor(PlayerActor iPlayer) : base(iPlayer.actorName, iPlayer.actorDescription, iPlayer.GetActorType(), iPlayer.GetAttackType())
    {

    }
}
