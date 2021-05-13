/******************************************************************************
 * This Class is the quest class. It uses a scriptable quest data to create a 
 * quest object for quest manager to keep track of for the players to compelte
 * It also helps facilitate saving and loading of quests
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

[System.Serializable]
public class Quest
{
    public int questID;
    public bool Repeatable; //If not repeatable check that timesCompleted is not >= 1

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal goal;

    private int timeCompleted = 0;

    public void Complete()
    {
        timeCompleted++;
    }
}
