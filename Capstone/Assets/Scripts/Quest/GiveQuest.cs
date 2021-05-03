using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class GiveQuest : MonoBehaviour
{
    public Quest quest;
    public Player player;

    public void AcceptQuest()
    {
        quest.isActive = true; //sets quest as active
    }
}
