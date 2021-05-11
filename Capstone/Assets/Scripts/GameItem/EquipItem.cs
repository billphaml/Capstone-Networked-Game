using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="New Game Equipment", menuName = "GameItem/Items/Equipment")]
public class EquipItem : GameItem
{
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

     void Awake()
    {
        //gameItemType = itemType.HEAD;
    }

    public override void useItem()
    {
        base.useItem();
        // theEquipmentManager.equipItem(this);
        // Equip the item
        // Remove from game
    }

    public EquipItem GetThis()
    {
        return this;
    }

    public void removeFromInventory()
    {
        //inventory.removeItem(this);
    }
}
