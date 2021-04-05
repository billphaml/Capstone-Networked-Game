using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;


public class NetworkPlayer : NetworkBehaviour
{
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public override void NetworkStart()
    {
        Move(Vector3.zero);
    }

    public void Move(Vector3 newPosition)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            var randomPosition = newPosition;
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }
        else
        {
            SubmitPositionRequestServerRpc(newPosition);
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(Vector3 newPosition, ServerRpcParams rpcParams = default)
    {
        Position.Value = newPosition;
    }

    void Update()
    {
        transform.position = Position.Value;
    }
}
