using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class HealthDemoTarget : NetworkBehaviour
{
    public NetworkVariable<float> Health = new NetworkVariable<float>(100f);

    [ServerRpc(RequireOwnership = false)]
    public void AddHealthServerRpc(float value)
    {
        Debug.Log("Adding health...");
        Health.Value += value;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RemoveHealthServerRpc(float value)
    {
        Debug.Log("Removing health...");
        Health.Value -= value;
    }
}
