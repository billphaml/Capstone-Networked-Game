/******************************************************************************
 * This Class is the logic for our in game chat and its behavior.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using TMPro;
using System;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;

    public TextMeshProUGUI nameTag;

    private string playerName = "";

    //private NetworkVariable<string> PlayNameSync = new NetworkVariable<string>("");

    private bool isConnected = false;

    private static event Action<string> OnMessage;

    public override void NetworkStart()
    {
        if (IsClient || IsHost)
        {
            base.NetworkStart();

            chatUI.SetActive(true);

            OnMessage += HandleNewMessage;

            if (IsOwner) Debug.Log("hello");

            playerName = GameObject.FindGameObjectWithTag("Game Network Manager").GetComponent<GameNetworkManager>().playerNickName;

            nameTag.text = playerName;

            isConnected = true;
        } 
        else
        {
            return;
        }
    }

    //private void Update()
    //{
    //    if (playerName == "")
    //    {
    //        if (isConnected)
    //        {
    //            if (IsLocalPlayer)
    //            {
    //                playerName = GameObject.FindGameObjectWithTag("Game Network Manager").GetComponent<GameNetworkManager>().playerNickName;
    //                nameTag.text = playerName;
    //            }
    //        }
    //    }
    //}

    //    if (playerName == "")
    //{
    //    if (isConnected)
    //    {
    //        if (IsLocalPlayer)
    //        {
    //            playerName = GameNetworkManager.playerNickName;
    //            ///PlayNameSync.Value = playerName;
    //            //nameTag.text = playerName;
    //        }
    //    }
    //}

    private void OnDestroy()
    {
        if (IsClient || IsHost)
        {
            OnMessage -= HandleNewMessage;
        }
        else
        {
            return;
        } 
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    public void Send()
    {
        if (IsClient || IsHost)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) return;

            string message = inputField.text;

            if (string.IsNullOrWhiteSpace(message)) return;

            SendMessageServerRpc(playerName, message);

            inputField.text = string.Empty;
        }
        else
        {
            return;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendMessageServerRpc(string player, string message)
    {
        // Need to somehow get nickname, perhaps make loadbalanceclient public in photon class and directly
        // retrieve nickname from there? Better idea is probably to have a custom nickname system stored
        // as network var so that we're not dependent on photon
        //NetworkManager.Singleton.gameObject.GetComponent<pho>
        HandleMessageClientRpc($"[{player}]: {message}");
    }

    [ClientRpc]
    private void HandleMessageClientRpc(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
