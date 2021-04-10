using MLAPI;
using MLAPI.Transports.UNET;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.Party;
using PlayFab.ClientModels;
using UnityEditor.Build.Content;
using UnityEngine.Rendering;

public class GameNetworkManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UIManager;

    public void Awake() 
    {
        if (IsHeadless())
        {
            NetworkManager.Singleton.StartServer();
        }
        else 
        {
            NetworkManager.Singleton.StartClient();
        }
    }

    void Start()
    {
        // Log into playfab
        var request = new LoginWithCustomIDRequest { CustomId = UnityEngine.Random.value.ToString(), CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        string networkId = "<your network id>";
        PlayFabMultiplayerManager.Get().JoinNetwork(networkId);
        PlayFabMultiplayerManager.Get().OnNetworkJoined += OnNetworkJoined;
    }

    private void OnLoginFailure(PlayFabError error)
    {
    }

    private void OnNetworkJoined(object sender, string networkId)
    {
        // Print the Network ID so you can give it to the other client.
        Debug.Log("Network joined!");
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            // Default buttons for hosting and joining, replaced by custom buttons below
            //StartButtons();
        }
        else
        {
            StatusLabels();

            //SubmitNewPosition();

            QuitSession();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        //if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void SubmitNewPosition()
    {
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        {
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                out var networkedClient))
            {
                //var player = networkedClient.PlayerObject.GetComponent<NetworkPlayer>();
                //if (player)
                //{
                //    //player.Move();
                //}
            }
        }
    }

    static void QuitSession()
    {
        if (GUILayout.Button("Quit")) NetworkManager.Singleton.StopClient();
    }
    
    //-------------------------------------------------------------------//

    public static void Host()
    {
        UIManager ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
        //UNetTransport manager = GameObject.Find("Network Manager").GetComponent<UNetTransport>();
        Debug.Log(ui.ipBoxHost.GetComponent<TMP_InputField>().text);
        if (ui.ipBoxHost.GetComponent<TMP_InputField>().text.Length <= 0)
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = ui.ipBoxHost.GetComponent<TMP_InputField>().text;
        }

        NetworkManager.Singleton.StartHost();
    }

    public static void Join()
    {
        UIManager ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
        //UNetTransport manager = GameObject.Find("Network Manager").GetComponent<UNetTransport>();
        Debug.Log(ui.ipBoxClient.GetComponent<TMP_InputField>().text);
        if (ui.ipBoxClient.GetComponent<TMP_InputField>().text.Length <= 0)
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = ui.ipBoxClient.GetComponent<TMP_InputField>().text;
        }
        NetworkManager.Singleton.StartClient();
    }
    bool IsHeadless()
    {
        return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
    }

}
