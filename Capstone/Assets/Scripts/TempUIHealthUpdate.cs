using UnityEngine;
using MLAPI;

public class TempUIHealthUpdate : MonoBehaviour
{
    private PlayerHealth player = null;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            //player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject;
        }
    }
}
