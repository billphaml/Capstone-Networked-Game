using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class PickupTarget : NetworkBehaviour
{
    public bool isAvailable = true;

    public NetworkVariable<int> TotalSpawns = new NetworkVariable<int>(0);

    [ServerRpc(RequireOwnership = false)]
    public void TryPickUpServerRpc(ulong clientID)
    {
        if (isAvailable)
        {
            Debug.Log("Someone is requesting");
            isAvailable = false;
            TotalSpawns.Value += 1;
            PickUpClientRpc(clientID);
        }
    }

    [ClientRpc]
    public void PickUpClientRpc(ulong clientID)
    {
        if (NetworkManager.Singleton.LocalClientId == clientID)
        {
            NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<PickupPlayer>().RegisterPickup(gameObject.GetComponent<PickupTarget>());
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void MakeAvailableServerRpc()
    {
        isAvailable = true;
    }
}
