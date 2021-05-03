using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;


    public int goalAmount;
    public int currentAmount;

    public bool goalCompleted()
    {
        return (currentAmount >= goalAmount);
    }

    // this needs to be called to increment the quest (probably will be called from the enemy when it dies)
    //NEED to check if quest is active
    public void hostile1Killed()
    {
        if (goalType == GoalType.Kill)
        {
            currentAmount++;
        }
    }
}

public enum GoalType
{ 
    Kill,
    Gathering,
    Escort,
    YourMOM

}
