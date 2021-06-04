/******************************************************************************
 * This Class updates the health UI to reflect the players current health.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using TMPro;

public class TempUIHealthUpdate : NetworkBehaviour
{
    private PlayerHealth player = null;

    public TextMeshProUGUI ui = null;

    public CanvasGroup healthCanvas = null;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            try
            {
                player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject.GetComponent<PlayerHealth>();
            }
            catch
            {
            }
        }

        if (player != null)
        {
            healthCanvas.alpha = 1;
            ui.text = "Health: " + player.Health.Value + "/" + player.maxHealth;
        }
    }
}
