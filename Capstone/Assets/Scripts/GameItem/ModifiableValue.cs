using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModifiedEvent();
[System.Serializable]
public class ModifiableValue 
{
    [SerializeField]
    private int playerbaseValue;
    public int playerBaseValue { get { return playerbaseValue;} set { playerbaseValue = value; /*UpdateModifiedValue();*/ } }

    [SerializeField]
    private int modifiedvalue;
    public int ModifiedValue { get { return modifiedvalue; } private set { modifiedvalue = value; } }

    public List<IModifier> modifiers = new List<IModifier>();

    public event ModifiedEvent valueModified;

    public ModifiableValue(ModifiedEvent theEvent = null)
    {
        modifiedvalue = playerbaseValue;
        if(theEvent != null)
        {
            valueModified += theEvent;
        }
    }

    public void registerModEvent(ModifiedEvent theEvent)
    {
        valueModified += theEvent;
    }

    public void unRegisterModEvent(ModifiedEvent theEvent)
    {
        valueModified -= theEvent;
    }

    public void updateModifiedvalue()
    {
        var addvalue = 0;
        for(int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].addValue(ref addvalue);
        }
        ModifiedValue = playerbaseValue + addvalue;
        if(valueModified != null)
        {
            valueModified.Invoke();
        }
    }

    public void addModifier(IModifier iModifier)
    {
        modifiers.Add(iModifier);
        updateModifiedvalue();
    }

    public void removeModifier(IModifier iModifier)
    {
        modifiers.Remove(iModifier);
        updateModifiedvalue();
    }
}
