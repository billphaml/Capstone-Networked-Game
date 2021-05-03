using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool Repeatable; //If not repeatable check that timesCompleted is not >= 1

    public string title;
    public string description;
    public int experianceReward;
    public int goldReward;

    public QuestGoal goal;

    private int timesCompleated = 0;

    public void Compleate()
    {
        isActive = false;
        timesCompleated++;

    }
}
