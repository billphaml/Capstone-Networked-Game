using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestComplete : MonoBehaviour
{

    public static GiveQuest theGiveQuest;

    [SerializeField]
    public Quest theQuest;
    public GameObject questWindow;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questGold;
    public TextMeshProUGUI QuestExp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardQuest(Quest iQuest) 
    {
        theQuest = iQuest;
        questWindow.SetActive(true);
        questTitle.text = theQuest.title + "Complete!";
        questDescription.text = "You Earned:";
        questGold.text = theQuest.experienceReward.ToString() + " Gold";
        QuestExp.text = theQuest.goldReward.ToString() + " EXP";
    }
}
