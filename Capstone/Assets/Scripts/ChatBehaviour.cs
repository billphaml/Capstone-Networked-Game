/*
 * Bug: server host can't send messages in chat after users have joined.
 * Something to do with ownership.
 */

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using TMPro;
using System;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;

    private static event Action<string> OnMessage;

    public override void NetworkStart()
    {
        if (IsClient || IsHost)
        {
            base.NetworkStart();

            chatUI.SetActive(true);

            OnMessage += HandleNewMessage;
        } 
        else
        {
            return;
        }
    }

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

            SendMessageServerRpc(message);

            inputField.text = string.Empty;
        }
        else
        {
            return;
        }
    }

    [ServerRpc]
    private void SendMessageServerRpc(string message)
    {
        // Need to somehow get nickname, perhaps make loadbalanceclient public in photon class and directly
        // retrieve nickname from there? Better idea is probably to have a custom nickname system stored
        // as network var so that we're not dependent on photon
        //NetworkManager.Singleton.gameObject.GetComponent<pho>
        HandleMessageClientRpc($"[Player]: {message}");
    }

    [ClientRpc]
    private void HandleMessageClientRpc(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
