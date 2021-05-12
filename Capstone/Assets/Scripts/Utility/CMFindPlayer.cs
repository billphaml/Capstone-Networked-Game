/******************************************************************************
 * This Class is for the Cinimachine to find the player
 * 
 * ***************************************************************************/
using UnityEngine;
using MLAPI;
using Cinemachine;

public class CMFindPlayer : NetworkBehaviour
{
    private CinemachineVirtualCamera cam = null;
    private GameObject player = null;

    // Start is called before the first frame update
    public void Activate()
    {
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        player = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject;
        cam.enabled = true;
        cam.Follow = player.transform;
        cam.LookAt = player.transform;
    }
}
