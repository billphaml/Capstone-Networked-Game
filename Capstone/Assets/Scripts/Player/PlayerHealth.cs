
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System;

public class PlayerHealth : NetworkBehaviour
{
    // Grab the value from EnemyStats later
    [SerializeField] private float maxHealth = 100f;

    private NetworkVariable<float> Health = new NetworkVariable<float>(0f);

    // Used to trigger a death event in other code
    // Could be used to display a respawn ui
    //public static event EventHandler<DeathEventArgs> OnDeath;

    public bool IsDead => Health.Value == 0f;

    public override void NetworkStart()
    {
        base.NetworkStart();

        Health.Value = maxHealth;
    }

    private void OnDestroy()
    {
        if (IsServer || IsHost)
        {
            //OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
        }
    }

    [ServerRpc]
    public void AddHealthServerRpc(float value)
    {
        value = Mathf.Max(value, 0);

        Health.Value = Mathf.Min(Health.Value + value, maxHealth);
    }

    [ServerRpc]
    public void RemoveHealthServerRpc(float value)
    {
        value = Mathf.Max(value, 0);

        Health.Value = Mathf.Max(Health.Value - value, 0);

        if (Health.Value == 0)
        {
            HandleDeathClientRpc();
        }
    }

    [ClientRpc]
    private void HandleDeathClientRpc()
    {
        gameObject.SetActive(false);
    }
}
