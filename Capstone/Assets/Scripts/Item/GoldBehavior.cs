/******************************************************************************
 * Logic for interactions with gold pickup.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class GoldBehavior : ItemBehavior, ISerializationCallbackReceiver
{

    /// <summary>
    /// Make a call to every client and check against the clientID that
    /// won the pickup. If the id matches then request for the player to pickup
    /// this item.
    /// </summary>
    /// <param name="clientID"></param>
    [ClientRpc]
    public override void PickUpClientRpc(ulong clientID)
    {
        if (NetworkManager.Singleton.LocalClientId == clientID)
        {
            NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<PlayerStat>().AddGold(gameObject.GetComponent<ItemBehavior>());
        }
    }
}
