using UnityEngine;
using MLAPI;
using TMPro;

public class TempUIHealthUpdate : NetworkBehaviour
{
    private PlayerHealth player = null;

    public TextMeshProUGUI ui = null;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject.GetComponent<PlayerHealth>();
        }

        ui.text = "Health: " + player.Health.Value + "/100";
    }
}
