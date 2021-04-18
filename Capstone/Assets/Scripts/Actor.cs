using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Actor
{

    public enum actorType
    {
        PLAYER,
        ENEMY,
        NPC
    }

    public enum attackType
    {
        FIST,
        SWORD,
        DAGGER,
        GREATSWORD,
        BOW,
        MAGIC,
        BEAST,
        BOSS
    }

    // Basic Display Stats
    private int hitPoint = 50; // Stat determining how many hits the players can take before dying.
    private int currentHitpoint = 50; // Stat showing the player's current hitpoint.
    private int staminaPoint = 20; // Stat determining how many stamina the players has to use special attacks.
    private int currentStaminaPoint = 20; // stat showing the player's current stamina;
    private int manaPoint = 20; // Hidden Stat, most likely not going to be used. Use if we have magic expansion.
    private int currentManaPoint = 20; // stat showing the playuer's current mana.
    private int actorLevel = 1; // Stat determining the level of the player. Used to show progression.



    // Defining Traits
    public string actorName; // The actor's name
    public string actorDescription; //  The Description for the actor
    public actorType theActorType; // Determine whether the actor is NPC or not
    public attackType theAttackType; // Determine the attack type of the actor


    // Actor Statistic
    private int actorBaseStrength = 10; // Stat used to calculate the attacking stat.
    private int actorBaseMagic = 10; // Hidden Stat, most likely not going to be used. Maybe could be used to enhance item effects
    private int actorBaseDexterity = 10; // Stat used to calculate the critical chance of attacks and increase stamina.
    private int actorBaseConstitution = 10; // Stat used to calculate the hitpoints and resistance

    // Actor Game Value Statistic
    private int actorBaseAttack = 10; // Used to calculate the damage dealt
    private int actorBaseDefense = 10; // Used to calculate the damage resisted
    private int actorBaseMagicResistance = 10; // Used to calculate the magic damage resisted
    private float actorBaseSpeed = 4.5f; // Used to calculate the movement speed
    private float actorBaseCriticalChance = 0.15f; // Used to calculate how often they crit
    private float actorBaseCriticalDamage = 1f; // Used to calculate the crit damage
    private float actorBaseResistance = 0.2f; // used to calculate how often status effect apply

    // This method is a basic constructor for the actor class.
    public Actor(string name, string description, actorType theType, attackType theAttack)
    {
        actorName = name;
        actorDescription = description;
        theActorType = theType;
        theAttackType = theAttack;
    }

    public void setActorType(actorType theType)
    {
        theActorType = theType;
    }

    public actorType getActorType()
    {
        return theActorType;
    }

    public void setAttackType(attackType theType)
    {
        theAttackType = theType;
    }

    public attackType getAttackType()
    {
        return theAttackType;
    }

    //
    public void setMaxHitPoint(int iHP)
    {
        if (iHP > 0)
        {
            hitPoint = iHP;
        }
    }

    //
    public int getMaxHitPoint()
    {
        return hitPoint;
    }

    //
    public void setHitPoint(int iHP)
    {
        if ((currentHitpoint + iHP) > getMaxHitPoint())
        {
            currentHitpoint = getMaxHitPoint();
        }
        else
        {
            currentHitpoint += iHP;
        }
    }

    //
    public int getHitPoint()
    {
        return hitPoint;
    }

    //
    public void setMaxStamina(int iStam)
    {
        if (iStam >= 0)
        {
            staminaPoint = iStam;
        }
    }

    //
    public int getMaxStamina()
    {
        return staminaPoint;
    }

    //
    public void setStamina(int iStam)
    {
        if ((currentStaminaPoint + iStam) > getMaxStamina())
        {
            currentStaminaPoint = getMaxStamina();
        }
        else if ((currentStaminaPoint + iStam) < 0)
        {

        }
        else
        {
            currentStaminaPoint += iStam;
        }
    }

    public int getStamina()
    {
        return currentStaminaPoint;
    }

    public void setMaxMana(int iMana)
    {
        if (iMana > 0)
        {
            manaPoint = iMana;
        }
    }

    public int getMaxMana()
    {
        return manaPoint;
    }

    public void setMana(int iMana)
    {
        if ((currentManaPoint + iMana) > getMaxMana())
        {
            currentManaPoint = getMaxMana();

        } else if ((currentManaPoint + iMana) < 0)
        {

        }
        else
        {
            currentManaPoint += iMana;
        }
    }



    public void setLevel(int iLevel)
    {
        if (iLevel > 0)
        {
            actorLevel = iLevel;
        }
    }

    public void addLevel()
    {
        if (actorLevel < 100)
        {
            actorLevel++;
        }
    }

    public int getLevel()
    {
        return actorLevel;
    }

    public void setStrength(int iStrength)
    {
        if (iStrength <= 0)
        {
            actorBaseStrength = 1;
        }
        else
        {
            actorBaseStrength = iStrength;
        }
    }

    public int getStrength()
    {
        return actorBaseStrength;
    }

    public void setMagic(int iMagic)
    {
        if (iMagic <= 0)
        {
            actorBaseMagic = 1;
        }
        else
        {
            actorBaseMagic = iMagic;
        }

    }

    public int getMagic()
    {
        return actorBaseMagic;
    }

    public void setDexterity(int iDex)
    {
        if (iDex <= 0)
        {
            actorBaseDexterity = 1;
        }
        else
        {
            actorBaseDexterity = iDex;
        }
    }

    public int getDexterity()
    {
        return actorBaseDexterity;
    }

    public void setConstitution(int iCon)
    {
        if(iCon <= 0)
        {
            actorBaseConstitution = 1;
        }
        else
        {
            actorBaseConstitution = iCon;
        }
    }

    public int getConstitution()
    {
        return actorBaseConstitution;
    }

    public void setAttack(int iAttack)
    {
        if(iAttack < 0)
        {
            actorBaseAttack = 0;
        }
        else
        {
            actorBaseAttack = iAttack;
        }
    }

    public int getAttack()
    {
        return actorBaseAttack;
    }

    public void setDefense(int iDefense)
    {
        if(iDefense < 0)
        {
            actorBaseDefense = 0;
        }
        else
        {
            actorBaseDefense = iDefense;
        }
    }

    public int getDefense()
    {
        return actorBaseDefense;
    }

    public void setMagicResistance(int iResist)
    {
        if(iResist < 0)
        {
            actorBaseMagicResistance = 0;
        }
        else
        {
            actorBaseMagicResistance = iResist;
        }
    }

    public int getMagicResistance()
    {
        return actorBaseMagicResistance;
    }

    public void setSpeed(float iSpeed)
    {
        if (iSpeed < 0)
        {
            actorBaseSpeed = 0.1f;
        }
        else if (iSpeed > 7.5f)
        {
            actorBaseSpeed = 7.5f;
        }
        else
        {
            actorBaseSpeed = iSpeed;
        }
    }

    public float getSpeed()
    {
        return actorBaseSpeed;
    }

    // Multiply the float by 100.
    public void setCritChance(float iCrit)
    {
        if(iCrit < 0.15f)
        {
            actorBaseCriticalChance = 0.15f;
        } else if(iCrit > 1f)
        {
            actorBaseCriticalChance = 1f;
        }
        else
        {
            actorBaseCriticalChance = iCrit;
        }
    }

    public float getCritChance()
    {
        return actorBaseCriticalChance;
    }

    public void setCritDamage(float iCrit)
    {
        if(iCrit < 0.01)
        {
            actorBaseCriticalDamage = 0f;
        } else if(iCrit > 4.5f)
        {
            actorBaseCriticalDamage = 4.5f;
        }
        else
        {
            actorBaseCriticalDamage = iCrit;
        }
    }

    public float getCritDamage()
    {
        return actorBaseCriticalDamage;
    }


    public void setResistance(float iResist)
    {
        if (iResist > 1f)
        {
            actorBaseResistance = 1f;
        }
        else if (iResist < 0.2f)
        {
            actorBaseResistance = 0.2f;
        }
        else 
        {
            actorBaseResistance = iResist;
        }
    }

    public float getResistance()
    {
        return actorBaseResistance;
    }

}
