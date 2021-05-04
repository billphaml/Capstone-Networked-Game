using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalGameManager : MonoBehaviour
{
    public static LocalGameManager theLocalGameManager;

     void Awake()
    {
        theLocalGameManager = this;
    }


    public List<Quest> currentQuest = new List<Quest>();
    public int maxQuest = 5;

    public GameObject questUI;
    public TextMeshProUGUI questText;

    public void UpdateKillQuest()
    {
       for(int i = 0; i < currentQuest.Count; i++)
        {
            if(currentQuest[i].isActive)
            currentQuest[i].goal.killCount();

            if (currentQuest[i].goal.goalCompleted())
            {
                // add experience to the player
                // add gold to the player
                currentQuest[i].complete();
                currentQuest.Remove(currentQuest[i]);
            }

        }
        updateQuestInfo();
    }

    public bool addQuest(Quest theQuest)
    {
        if(currentQuest.Count < 1)
        {
            currentQuest.Add(theQuest);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void updateQuestInfo()
    {
        questInfoHandler();
    }

    private void questInfoHandler()
    {
        if(currentQuest.Count <= 0)
        {
            questUI.SetActive(false);
        }
        else
        {
            questUI.SetActive(true);
            if (currentQuest.Count > 0)
            {
                questText.text = currentQuest[0].title + " (" + currentQuest[0].goal.currentAmount + "/" + currentQuest[0].goal.goalAmount + ")";
            }
        }
    }
}
