/******************************************************************************
 * Reuses enemy damage script to detect if play has hit the puzzle trigger.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;

public class PuzzleDamagable : EnemyDamageable
{
    [SerializeField] private PuzzleOnHit trigger;

    public override void Start()
    {
    }

    public override void DealDamage(float damageToDeal)
    {
        trigger.HitServerRpc();
    }
}
