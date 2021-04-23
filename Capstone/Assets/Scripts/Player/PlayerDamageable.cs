
using UnityEngine;
using MLAPI;

public class PlayerDamageable : NetworkBehaviour
{
    [SerializeField] private PlayerHealth health = null;

    public override void NetworkStart()
    {
        base.NetworkStart();

        health = gameObject.GetComponent<PlayerHealth>();
    }

    public void DealDamage(float damageToDeal)
    {
        health.RemoveHealthServerRpc(damageToDeal);
    }
}
