/******************************************************************************
 * Health script that should be added to objects that should take or heal
 * damage. Use only for objects that a enemy will be attacking. This script
 * only manages health. To take or heal damage add the corresponding enemy
 * scripts.
 * 
 * Authors: Bill, Hamza, Max, Ryan
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
    [SerializeField] public float maxHealth;

    [SerializeField] private PlayerStat thePlayer;

    public GameObject respawnMenu;

    public SpriteRenderer playerSprite;

    public SpriteRenderer shadowSprite;

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
    private void Start()
    {
        if (thePlayer == null)
        {
            thePlayer = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.GetComponent<PlayerStat>();

            respawnMenu = GameObject.FindGameObjectWithTag("Respawn Menu");

            if (thePlayer != null) {
                thePlayer.statUpdated += updateHealthClientRpc;
                maxHealth = thePlayer.thePlayer.playerMaxHealth;
                Health.Value = maxHealth;
            }
        }
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
    [ServerRpc(RequireOwnership = false)]
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
    [ServerRpc(RequireOwnership = false)]
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
        if (IsLocalPlayer)
        {
            respawnMenu.GetComponent<CanvasGroup>().alpha = 1;
            respawnMenu.GetComponent<CanvasGroup>().interactable = true;
            respawnMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    [ClientRpc]
    private void updateHealthClientRpc()
    {
        float dummyMaxHealth = maxHealth;
        maxHealth = thePlayer.thePlayer.playerMaxHealth;

        if(maxHealth > dummyMaxHealth)
        {
            Health.Value += (maxHealth - dummyMaxHealth);
        }

        if (maxHealth < Health.Value)
        {
            Health.Value = maxHealth;
        }
    }
}
