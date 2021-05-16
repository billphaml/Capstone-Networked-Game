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
    //Quest constructor for importing from a scriptable object
    public Quest(ScriptableQuest import) {
        this.questID = import.questID;
        this.Repeatable = import.Repeatable;
        this.title = import.title;
        this.description = import.description;
        this.experienceReward = import.experienceReward;
        this.goldReward = import.goldReward;
        this.goal = import.goal;

    }
    //Quest constructor for importing all feilds
    public Quest(int ID, bool rep, string tit, string desc, int exp, int gold, QuestGoal gol) {
        questID = ID;
        Repeatable = rep;
        title = tit;
        description = desc;
        experienceReward = exp;
        goldReward = gold;
        goal = gol;
    }

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
