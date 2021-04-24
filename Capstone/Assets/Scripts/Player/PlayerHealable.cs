/******************************************************************************
 * 
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class PlayerHealable : NetworkBehaviour
{
    /// <summary>
    /// Reference to health component. Make sure one exists on this object.
    /// </summary>
    [SerializeField] private PlayerHealth health = null;

    /// <summary>
    /// Similar to awake but for occurs when all clients are synced.
    /// </summary>
    public override void NetworkStart()
    {
        base.NetworkStart();

        health = gameObject.GetComponent<PlayerHealth>();
    }

    /// <summary>
    /// Calls a rpc in health to restore health equal to the passed in amount.
    /// </summary>
    /// <param name="amountToheal"></param>
    public void Heal(float amountToheal)
    {
        health.AddHealthServerRpc(amountToheal);
    }
}
