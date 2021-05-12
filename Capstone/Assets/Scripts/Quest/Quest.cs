/******************************************************************************
 * This Class is the overall quest. It contains variables that help us determine
 * what type of quest it is as well as if it is active, repeatable, and the
 * rewards.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Event/Quest")]
[System.Serializable]
public class Quest: ScriptableObject
{
    public bool isActive;
    public bool Repeatable; //If not repeatable check that timesCompleted is not >= 1

    public string title;
    [TextArea(5, 10)]
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal goal;

    private int timeCompleted = 0;

    public void complete()
    {
        isActive = false;
        timeCompleted++;
    }
}
