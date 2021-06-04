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
    private PlayerStat player = null;

    public TextMeshProUGUI ui = null;

    public CanvasGroup goldCanvas;

    // Update is called once per frame
    void Update()
    {
        //ui.text = "Gold";
        if (player == null)
        {
            try
            { 
                player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject.GetComponent<PlayerStat>();
            }
            catch
            {
                ui.text = "Gold";
            }
        }

        if (player != null)
        {
            goldCanvas.alpha = 1;
            ui.text = "Gold: " + player.GetGold();
        }
    }

    public void GoldAdd(int input)
    {
        player.AddGold(input);
    }
    public void GoldRemove(int input)
    {
        player.RemoveGold(input);
    }
}
