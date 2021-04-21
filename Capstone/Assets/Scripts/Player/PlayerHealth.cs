using System;
using UnityEngine;
using MLAPI;

public class PlayerHealth : NetworkBehaviour
{
    //// Grab the value from EnemyStats later
    //[SerializeField] private float maxHealth = 100f;

    //[SyncVar(hook = nameof(HandleHealthUpdated))]
    //private float health = 0f;

    //public static event EventHandler<DeathEventArgs> OnDeath;
    //public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

    //public bool IsDead => health == 0f;

    //public override void OnStartServer()
    //{
    //    health = maxHealth;
    //}

    //[ServerCallback]
    //private void OnDestroy()
    //{
    //    OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
    //}

    //[Server]
    //public void Add(float value)
    //{
    //    value = Mathf.Max(value, 0);

    //    health = Mathf.Min(health + value, maxHealth);
    //}

    //[Server]
    //public void Remove(float value)
    //{
    //    value = Mathf.Max(value, 0);

    //    health = Mathf.Max(health - value, 0);

    //    if (health == 0)
    //    {
    //        OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });

    //        RpcHandleDeath();
    //    }
    //}

    //private void HandleHealthUpdated(float oldValue, float newValue)
    //{
    //    OnHealthChanged?.Invoke(this, new HealthChangedEventArgs
    //    {
    //        health = health,
    //        maxHealth = maxHealth
    //    });
    //}

    //[ClientRpc]
    //private void RpcHandleDeath()
    //{
    //    gameObject.SetActive(false);
    //}
}
