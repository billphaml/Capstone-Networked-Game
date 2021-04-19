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
        if (!IsClient) return;

        base.NetworkStart();

        chatUI.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    private void OnDestroy()
    {
        if (!IsClient) return;

        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    public void Send(string message)
    {
        if (!IsClient) return;

        if (!Input.GetKeyDown(KeyCode.Return)) return;

        if (string.IsNullOrWhiteSpace(message)) return;

        SendMessageServerRpc(message);

        inputField.text = string.Empty;
    }

    [ServerRpc]
    private void SendMessageServerRpc(string message)
    {
        HandleMessageClientRpc($"[Player]: {message}");
    }

    [ClientRpc]
    private void HandleMessageClientRpc(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
