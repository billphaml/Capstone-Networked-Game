/******************************************************************************
 * This Class contains the logic to set, open, accept, and close a quest.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

[System.Serializable]
public class GiveQuest : MonoBehaviour
{
    public static GiveQuest theGiveQuest;

     void Awake()
    {
            theGiveQuest = this;
    }

    [SerializeField]
    public Quest theQuest;
    public GameObject questWindow;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questGold;
    public TextMeshProUGUI QuestExp;

    public void setQuest(Quest iQuest)
    {
        theQuest = iQuest;
    }

    public void openQuest()
    {
        questWindow.SetActive(true);
        questTitle.text = theQuest.title;
        questDescription.text = theQuest.description;
        questGold.text = theQuest.experienceReward.ToString() + " Gold";
        QuestExp.text =  theQuest.goldReward.ToString() + " EXP";
    }


    public void acceptQuest()
    {
        questWindow.SetActive(false);
        theQuest.isActive = true;
        LocalGameManager.theLocalGameManager.addQuest(theQuest);
        LocalGameManager.theLocalGameManager.updateQuestInfo();
    }

    public void closeQuest()
    {
        questWindow.SetActive(false);
    }

}
