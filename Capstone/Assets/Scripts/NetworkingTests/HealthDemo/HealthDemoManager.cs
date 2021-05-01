/*
 * TODO:
 * - Look up name spaces and how to use them to organize these demo scripts
 */

using MLAPI;
using UnityEngine;

public class HealthDemoManager : NetworkBehaviour
{
    /// <summary>
    /// Enable to have the client continuously make requests to update health.
    /// </summary>
    [SerializeField] private bool isRequesting = false;

    [SerializeField] private HealthDemoTarget health = null;

    [SerializeField] private HealthDemoPlayer player = null;

    private float nextRequestTime = 0f;

    private float requestDelayTime = 3f;

    private int position = 0;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 400, 400));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();

            StartEndRequesting();

            ChangePlayerPosition();
        }

        GUILayout.EndArea();
    }

    void StartButtons()
    {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
    }

    void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
        GUILayout.Label("Requesting: " + isRequesting);
        GUILayout.Label("Target health: " + health.Health.Value);
    }

    void StartEndRequesting()
    {
        if (GUILayout.Button("Change Position"))
        {
            position++;
            if (position > 3) position = 0;

            switch (position)
            {
                case 0:
                    player.transform.position = new Vector2(3, 3);
                    break;
                case 1:
                    player.transform.position = new Vector2(3, -3);
                    break;
                case 2:
                    player.transform.position = new Vector2(-3, -3);
                    break;
                case 3:
                    player.transform.position = new Vector2(-3, 3);
                    break;
            }
        }
    }

    void ChangePlayerPosition()
    {
        if (GUILayout.Button("Request"))
        {
            isRequesting = (isRequesting) ? false : true;
        }
    }

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthDemoTarget>();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
            out var networkedClient))
            {
                var localPlayer = networkedClient.PlayerObject.GetComponent<HealthDemoPlayer>();
                if (localPlayer)
                {
                    player = localPlayer;
                }
            }
        }

        if (isRequesting)
        {
            if (nextRequestTime <= Time.time)
            {
                Debug.Log("Requesting...");
                player.Request();
                nextRequestTime = Time.time + requestDelayTime;
            }
        }

        Debug.Log("Health: " + health.Health.Value);
    }
}
