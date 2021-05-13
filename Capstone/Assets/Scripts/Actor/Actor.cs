/******************************************************************************
 * This class is the base class from which all actors derive from. Actors are
 * classes which contain information about the object such as it's stats,
 * items, dialogue, etc.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

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

    public void SetActorType(actorType theType)
    {
        theActorType = theType;
    }

    public actorType GetActorType()
    {
        return theActorType;
    }

    public void SetAttackType(attackType theType)
    {
        theAttackType = theType;
    }

    public attackType GetAttackType()
    {
        return theAttackType;
    }

    public void SetMaxHitPoint(int iHP)
    {
        if (iHP > 0)
        {
            hitPoint = iHP;
        }
    }

    public int GetMaxHitPoint()
    {
        return hitPoint;
    }

    public void SetHitPoint(int iHP)
    {
        if ((currentHitpoint + iHP) > GetMaxHitPoint())
        {
            currentHitpoint = GetMaxHitPoint();
        }
        else
        {
            currentHitpoint += iHP;
        }
    }

    public int GetHitPoint()
    {
        return hitPoint;
    }

    public void SetMaxStamina(int iStam)
    {
        if (iStam >= 0)
        {
            staminaPoint = iStam;
        }
    }

    public int GetMaxStamina()
    {
        return staminaPoint;
    }

    public void SetStamina(int iStam)
    {
        if ((currentStaminaPoint + iStam) > GetMaxStamina())
        {
            currentStaminaPoint = GetMaxStamina();
        }
        else if ((currentStaminaPoint + iStam) < 0)
        {

        }
        else
        {
            currentStaminaPoint += iStam;
        }
    }

    public int GetStamina()
    {
        return currentStaminaPoint;
    }

    public void SetMaxMana(int iMana)
    {
        if (iMana > 0)
        {
            manaPoint = iMana;
        }
    }

    public int GetMaxMana()
    {
        return manaPoint;
    }

    public void SetMana(int iMana)
    {
        if ((currentManaPoint + iMana) > GetMaxMana())
        {
            currentManaPoint = GetMaxMana();

        } else if ((currentManaPoint + iMana) < 0)
        {

        }
        else
        {
            currentManaPoint += iMana;
        }
    }

    public void SetLevel(int iLevel)
    {
        if (iLevel > 0)
        {
            actorLevel = iLevel;
        }
    }

    public void AddLevel()
    {
        if (actorLevel < 100)
        {
            actorLevel++;
        }
    }

    public int GetLevel()
    {
        return actorLevel;
    }

    public void SetStrength(int iStrength)
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

    public int GetStrength()
    {
        return actorBaseStrength;
    }

    public void SetMagic(int iMagic)
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

    public int GetMagic()
    {
        return actorBaseMagic;
    }

    public void SetDexterity(int iDex)
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

    public int GetDexterity()
    {
        return actorBaseDexterity;
    }

    public void SetConstitution(int iCon)
    {
        if (iCon <= 0)
        {
            actorBaseConstitution = 1;
        }
        else
        {
            actorBaseConstitution = iCon;
        }
    }

    public int GetConstitution()
    {
        return actorBaseConstitution;
    }

    public void SetAttack(int iAttack)
    {
        if (iAttack < 0)
        {
            actorBaseAttack = 0;
        }
        else
        {
            actorBaseAttack = iAttack;
        }
    }

    public int GetAttack()
    {
        return actorBaseAttack;
    }

    public void SetDefense(int iDefense)
    {
        if (iDefense < 0)
        {
            actorBaseDefense = 0;
        }
        else
        {
            actorBaseDefense = iDefense;
        }
    }

    public int GetDefense()
    {
        return actorBaseDefense;
    }

    public void SetMagicResistance(int iResist)
    {
        if (iResist < 0)
        {
            actorBaseMagicResistance = 0;
        }
        else
        {
            actorBaseMagicResistance = iResist;
        }
    }

    public int GetMagicResistance()
    {
        return actorBaseMagicResistance;
    }

    public void SetSpeed(float iSpeed)
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

    public float GetSpeed()
    {
        return actorBaseSpeed;
    }

    // Multiply the float by 100.
    public void SetCritChance(float iCrit)
    {
        if (iCrit < 0.15f)
        {
            actorBaseCriticalChance = 0.15f;
        } else if (iCrit > 1f)
        {
            actorBaseCriticalChance = 1f;
        }
        else
        {
            actorBaseCriticalChance = iCrit;
        }
    }

    public float GetCritChance()
    {
        return actorBaseCriticalChance;
    }

    public void SetCritDamage(float iCrit)
    {
        if (iCrit < 0.01)
        {
            actorBaseCriticalDamage = 0f;
        } else if (iCrit > 4.5f)
        {
            actorBaseCriticalDamage = 4.5f;
        }
        else
        {
            actorBaseCriticalDamage = iCrit;
        }
    }

    public float GetCritDamage()
    {
        return actorBaseCriticalDamage;
    }

    public void SetResistance(float iResist)
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

    public float GetResistance()
    {
        return actorBaseResistance;
    }
}
