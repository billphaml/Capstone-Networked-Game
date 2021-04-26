/*
 * TODO:
 * - Look up name spaces and how to use them to organize these demo scripts
 */

using MLAPI;
using UnityEngine;

public class MovementTestManager : NetworkBehaviour
{
    /// <summary>
    /// Enable to have the client continuously make requests to update health.
    /// </summary>
    [SerializeField] private bool isRequesting = false;

    [SerializeField] private Transform location = null;

    [SerializeField] private MovementTestPlayer player = null;

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
        GUILayout.Label("Target Position: " + location.position.ToString());
    }

    void StartEndRequesting()
    {
        if (GUILayout.Button("Request"))
        {
            isRequesting = (isRequesting) ? false : true;
        }
    }

    private void Update()
    {
        if(location == null)
       location = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
            out var networkedClient))
            {
                var localPlayer = networkedClient.PlayerObject.GetComponent<MovementTestPlayer>();
                if (localPlayer)
                {
                    player = localPlayer;
                }
            }
        }

        if (isRequesting)
        {
            Debug.Log("requesting...");
            player.Request();
        }

        Debug.Log("Position: " + location.position.ToString());
    }
}
