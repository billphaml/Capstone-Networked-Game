using MLAPI;
using UnityEngine;

public class SpawnDemoManager : NetworkBehaviour
{
    [SerializeField] private bool isSpawning = false;

    [SerializeField] private GameObject prefab = null;

    public int spawnCount = 0;

    private float nextSpawnTime = 0f;

    private float spawnDelayTime = 0.6f;

    GUIStyle headstyle = new GUIStyle();

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

            StartEndSpawning();
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
        GUILayout.Label("Spawning: " + isSpawning, headstyle);
        GUILayout.Label("Spawn count: " + spawnCount, headstyle);
    }

    void StartEndSpawning()
    {
        if (IsHost)
        {
            if (GUILayout.Button("Spawn"))
            {
                isSpawning = (isSpawning) ? false : true;
            }
        }
    }

    public Vector3 randomPos()
    {
        return new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
    }

    private void Start()
    {
        headstyle.fontSize = 24;
        headstyle.normal.textColor = Color.white;
    }

    private void FixedUpdate()
    {
        if (isSpawning && IsHost)
        {
            if (nextSpawnTime <= Time.time)
            {
                Debug.Log("Spawning...");
                GameObject temp = Instantiate(prefab, randomPos(), Quaternion.identity);
                temp.GetComponent<NetworkObject>().Spawn();
                nextSpawnTime = Time.time + spawnDelayTime;
            }
        }
    }
}
