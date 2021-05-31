/******************************************************************************
 * This Class is used as a local manager for the client side gameplay, mostly
 * used to update quests and quest related stuff.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using MLAPI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalGameManager : MonoBehaviour
{
    public GameObject player = null;
    public GameObject respawnMenu;

    public static LocalGameManager theLocalGameManager;
    public GoldUpdater iLoveGooooold;
    public QuestComplete UI;
    void Awake()
    {
        theLocalGameManager = this;
    }

    private void Start()
    {
        respawnMenu = GameObject.FindGameObjectWithTag("Respawn Menu");
    }

    private void Update()
    {
        if (player == null)
        {
            try
            {
                player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject;
            }
            catch
            {
            }
        }
    }

    public void EnablePlayer()
    {
        player.SetActive(true);
        player.transform.position = Vector3.zero;
        var hp = player.GetComponent<PlayerHealth>().maxHealth;
        player.GetComponent<PlayerHealable>().Heal(hp);
        respawnMenu.GetComponent<CanvasGroup>().alpha = 0;
        respawnMenu.GetComponent<CanvasGroup>().interactable = false;
        respawnMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public List<Quest> currentQuest = new List<Quest>();
    public int maxQuest = 5;

    public GameObject questUI;
    public TextMeshProUGUI questText;

    public void UpdateKillQuest()
    {
        for(int i = 0; i < currentQuest.Count; i++)
        {
            //if(currentQuest[i].isActive)
            currentQuest[i].goal.KillCount();

            if (currentQuest[i].goal.goalCompleted())
            {
                // add experience to the player
                // add gold to the player
                iLoveGooooold.GoldAdd(currentQuest[i].goldReward);
                UI.RewardQuest(currentQuest[i]);

                currentQuest[i].Complete();
                currentQuest.Remove(currentQuest[i]);
            }
        }
        UpdateQuestInfo();
    }

    public bool AddQuest(ScriptableQuest theQuest)
    {
        if (currentQuest.Count < 1)
        {
            // Convert Scriptable Quest into Quest and add it into the currentQuestList
            currentQuest.Add(new Quest(theQuest));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateQuestInfo()
    {
        QuestInfoHandler();
    }

    private void QuestInfoHandler()
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
                questText.text = currentQuest[0].title + " (" + currentQuest[0].goal.currentAmount + "/" + currentQuest[0].goal.GoalAmount + ")";
            }
        }
    }

    public List<Quest> saveQuests() 
    {
        return currentQuest;
    }
}
