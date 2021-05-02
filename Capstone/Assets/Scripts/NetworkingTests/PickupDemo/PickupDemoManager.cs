/*
 * TODO:
 * - Look up name spaces and how to use them to organize these demo scripts
 */

using MLAPI;
using UnityEngine;

public class PickupDemoManager : NetworkBehaviour
{
    /// <summary>
    /// Enable to have the client continuously make requests to update health.
    /// </summary>
    [SerializeField] private bool isRequesting = false;

    [SerializeField] private PickupTarget target = null;

    [SerializeField] private PickupPlayer player = null;

    private float nextRequestTime = 0f;

    private int position = 0;

    GUIStyle headstyle = new GUIStyle();

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
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name, headstyle);
        GUILayout.Label("Mode: " + mode, headstyle);
        GUILayout.Label("Requesting: " + isRequesting, headstyle);
        GUILayout.Label("Total local item pickups: " + player.pickups, headstyle);
        GUILayout.Label("Total items spawns: " + target.TotalSpawns.Value, headstyle);
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
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PickupTarget>();

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
                var localPlayer = networkedClient.PlayerObject.GetComponent<PickupPlayer>();
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
                nextRequestTime = Time.time + Random.Range(0.6f, 0.9f);
            }
        }
    }
}
