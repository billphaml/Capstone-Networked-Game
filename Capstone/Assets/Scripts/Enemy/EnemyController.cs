/******************************************************************************
 * Manager for enemy that holds references as well updates enemy logic.
 * Utilizes EnemyFSM, EnemyMovement, and EnemyInteraction classes
 * 
 * TODO:
 * - Add player searching to Update
 * - Add enemy health less than zero destroy logic
 * - Update enemy take damage logic
 * - Uncomment player distance chase check
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class EnemyController : NetworkBehaviour
{
    /// <summary>
    /// Reference to player object.
    /// </summary>
    public GameObject player = default;

    /// <summary>
    /// Reference to enemy fsm.
    /// </summary>
    public EnemyFSM fsm = default;

    /// <summary>
    /// Reference to enemy movement.
    /// </summary>
    public EnemyMovement move = default;

    void Start()
    {
        if (IsHost || IsServer)
        {
            // Remove after adding nearest player searching
            player = GameObject.FindGameObjectWithTag("Player");

            fsm = gameObject.GetComponent<EnemyFSM>();

            move = gameObject.GetComponent<EnemyMovement>();

            fsm.SetState(EnemyFSM.EnemyState.patrolState);
        }
    }

    void Update()
    {
        if (IsHost || IsServer)
        {
            // Add nearest player searching
            //player = 

            move.UpdateMovement();

            fsm.UpdateFSM();

            //if (Vector3.Distance(gameObject.transform.position, player.transform.position) < fsm.GetChaseRange())
            //{
            //    fsm.SetState(EnemyFSM.EnemyState.chaseState);

            //    if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= fsm.GetAttackRange())
            //    {
            //        fsm.SetState(EnemyFSM.EnemyState.attackState);
            //    }
            //}
        }
    }
}
