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

    GUIStyle headstyle = new GUIStyle();

    private float nextRequestTime = 0f;

    private float requestDelayTime = 0.3f;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 480, 480));

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
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name, headstyle);
        GUILayout.Label("Mode: " + mode, headstyle);
        GUILayout.Label("Requesting: " + isRequesting, headstyle);
        PlayerPositions();
    }

    void PlayerPositions()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < players.Length; i++)
        {
            GUILayout.Label("Player " + (i + 1) + " Position: " + players[i].transform.position, headstyle);
        }
    }

    void StartEndRequesting()
    {
        if (GUILayout.Button("Request"))
        {
            isRequesting = (isRequesting) ? false : true;
        }
    }

    private void Start()
    {
        headstyle.fontSize = 24;
        headstyle.normal.textColor = Color.white;
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
            if (nextRequestTime <= Time.time)
            {
                Debug.Log("requesting...");
                player.Request();
                nextRequestTime = Time.time + requestDelayTime;
            }
        }

        Debug.Log("Position: " + location.position.ToString());
    }
}
