/******************************************************************************
 * This Class updates the gold UI to reflect the players current wealth.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using MLAPI;
public class GoldUpdater : MonoBehaviour
{
    private PlayerActor player = null;

    public TextMeshProUGUI ui = null;

    // Update is called once per frame
    void Update()
    {
        //ui.text = "Gold";
        if (player == null)
        {
            try
            { 
                player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject.GetComponent<PlayerStat>().thePlayer;
            }
            catch
            {
                ui.text = "Gold";
            }
        }

        if (player != null)
        {
            ui.text = "Gold: " + player.gold;
        }
    }
}
