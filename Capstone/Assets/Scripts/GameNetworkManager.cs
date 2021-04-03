using MLAPI;
using UnityEngine;

namespace Capstone
{
    public class GameNetworkManager : MonoBehaviour
    {
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

                SubmitNewPosition();

                QuitSession();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
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
                    var player = networkedClient.PlayerObject.GetComponent<NetworkPlayer>();
                    if (player)
                    {
                        player.Move();
                    }
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
            NetworkManager.Singleton.StartHost();
        }

        public static void Join()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
