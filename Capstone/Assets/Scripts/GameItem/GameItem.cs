using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Item", menuName = "Item" )]
public class GameItem : ScriptableObject
{
    public enum itemType
    {
        HEAD,
        ARMOR,
        NECKLACE,
        RING,
        SWORD,
        GREATSWORD,
        DAGGER,
        BOW,
        MAGIC,
        CONSUME,
        IMPORTANT
    }

    // Item Description
    [Header("Game Item Description")]
    public string itemName;
    public string itemDescription;
    public itemType itemGameType;

    // Item add onto game value statistic
    [Header("Game Value Statistic")]
    public int addAttack;
    public int addDefence;
    public int addMagicResistance;
    public float addSpeed;
    public int addCriticalChance;
    public int addCriticalDamage;
    public int addResistance;


    // Item add onto actor statistic 
    [Header("Game Actor Statistic")]
    public int addStrength;
    public int addMagic;
    public int addDexterity;
    public int addConstitution;

    // Item add onto the Display Stats
    [Header("Game Display Statistic")]
    public int addHP;
    public int addSP;
    public int addMP;

    // Item specific stats
    [Header("Game Item Unique")]
    public float itemRange;
    public float itemRadius;
    public float itemAttackSpeed;

}
