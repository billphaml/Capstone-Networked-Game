/******************************************************************************
 * Logic for interactions with items such as pickup.
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ItemBehavior : NetworkBehaviour, ISerializationCallbackReceiver
{
    public GameItem theItem;

    /// <summary>
    /// Determines if the item is still available to be picked up.
    /// </summary>
    private bool isAvailable = true;

    /// <summary>
    /// Check if the item has been requested for pickup by another player. If
    /// it has then do nothing. If it hasn't then transfer ownership to the
    /// client and make a call for the client to pickup the item.
    /// </summary>
    /// <param name="clientID"></param>
    [ServerRpc(RequireOwnership = false)]
    public void TryPickUpServerRpc(ulong clientID)
    {
        if (isAvailable)
        {
            isAvailable = false;
            gameObject.GetComponent<NetworkObject>().ChangeOwnership(clientID);
            PickUpClientRpc(clientID);
        }
    }

    /// <summary>
    /// Make a call to every client and check against the clientID that
    /// won the pickup. If the id matches then request for the player to pickup
    /// this item.
    /// </summary>
    /// <param name="clientID"></param>
    [ClientRpc]
    public void PickUpClientRpc(ulong clientID)
    {
        if (NetworkManager.Singleton.LocalClientId == clientID)
        {
            NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<PlayerStat>().AddItem(gameObject.GetComponent<ItemBehavior>());
        }
    }

    /// <summary>
    /// Destroy gameobject on every client.
    /// </summary>
    [ServerRpc]
    public void DestroyItemObjectServerRpc()
    {
        Destroy(gameObject);
    }

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = theItem.itemImage;
    }
}
