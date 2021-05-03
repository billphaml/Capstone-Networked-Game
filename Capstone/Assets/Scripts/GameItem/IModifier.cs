using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier 
{
    void addValue(ref int baseValue);
    void addValue(ref float baseValue);
}
