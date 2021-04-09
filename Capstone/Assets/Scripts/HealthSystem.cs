using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class HealthSystem : NetworkBehaviour
{
    [SerializeField] private float maxHealth;
    NetworkVariableFloat health = new NetworkVariableFloat(100f);

    public void TakeDamage(float damage)
    {
        health.Value -= damage;
    }
}
