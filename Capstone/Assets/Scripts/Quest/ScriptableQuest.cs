/******************************************************************************
 * This Class is the quest scriptable data. It contains variables that help us 
 * determine what type of quest it is as well as if it is active, repeatable, 
 * and the rewards.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest", menuName = "Event/Quest")]
[System.Serializable]
public class ScriptableQuest : ScriptableObject
{
    public int questID;
    public bool Repeatable; //If not repeatable check that timesCompleted is not >= 1

    public string title;
    [TextArea(5, 10)]
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal goal;

}
