/******************************************************************************
 * Health script that should be added to objects that should take or heal
 * damage. Use only for objects that a enemy will be attacking. This script
 * only manages health. To take or heal damage add the corresponding enemy
 * scripts.
 * 
 * TODO:
 * - Retrieve max health value from PlayerController.PlayerStats.GetMaxHealth()
 *   or something along that line.
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System;

public class PlayerHealth : NetworkBehaviour
{
    // Grab the value from PlayerStats later
    /// <summary>
    /// Max health for this object.
    /// </summary>
    [SerializeField] private float maxHealth = 100f;

    /// <summary>
    /// Current health for this object.
    /// </summary>
    public NetworkVariable<float> Health = new NetworkVariable<float>(0f);

    // Used to trigger a death event in other code
    // Could be used to display a respawn ui
    //public static event EventHandler<DeathEventArgs> OnDeath;

    public bool IsDead => Health.Value == 0f;

    /// <summary>
    /// Similar to awake but for occurs when all clients are synced.
    /// </summary>
    public override void NetworkStart()
    {
        base.NetworkStart();

        Health.Value = maxHealth;
    }

    /// <summary>
    /// Called when player dies or is disconnected.
    /// </summary>
    private void OnDestroy()
    {
        if (IsServer || IsHost)
        {
            //OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
        }
    }

    /// <summary>
    /// Adds health to the object while clamping amount between 0 and
    /// maxHealth.
    /// </summary>
    /// <param name="value"></param>
    [ServerRpc]
    public void AddHealthServerRpc(float value)
    {
        value = Mathf.Max(value, 0);

        Health.Value = Mathf.Min(Health.Value + value, maxHealth);
    }

    /// <summary>
    /// Removes health from the object while clamping amount between 0 and
    /// MaxHealth. Calls rpc to handle death if health reaches 0.
    /// </summary>
    /// <param name="value"></param>
    [ServerRpc]
    public void RemoveHealthServerRpc(float value)
    {
        Debug.Log("Removing health");

        value = Mathf.Max(value, 0);

        Health.Value = Mathf.Max(Health.Value - value, 0);

        if (Health.Value == 0)
        {
            HandleDeathClientRpc();
        }
    }

    /// <summary>
    /// Destroys object on all clients.
    /// </summary>
    [ClientRpc]
    private void HandleDeathClientRpc()
    {
        gameObject.SetActive(false);
    }
}
