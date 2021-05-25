/******************************************************************************
 * Manager for enemy that holds references as well updates enemy logic.
 * Utilizes EnemyFSM, EnemyMovement, and EnemyInteraction classes
 * 
 *  * Note:
 * - Unity Editor mode is not considered a host. Enemy updates won't o
 * 
 * TODO:
 * - Add player searching to Update
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

// Uncomment for debug mode
#define DEBUG
// Uncomment for normal mode
//#undef DEBUG

using UnityEngine;
using MLAPI;
using System.Collections.Generic;
using System;

public class EnemyController : NetworkBehaviour
{
    /// <summary>
    /// Reference to player object.
    /// </summary>
    public GameObject player = null;

    /// <summary>
    /// Reference to enemy fsm.
    /// </summary>
    public EnemyFSM fsm = null;

    /// <summary>
    /// Reference to enemy movement.
    /// </summary>
    public EnemyMovement move = null;

    public override void NetworkStart()
    {
        //if (IsHost || IsServer)
        //{
        //    base.NetworkStart();
            
        //    // Remove after adding nearest player searching
        //    player = GameObject.FindGameObjectWithTag("Player");

        //    fsm = gameObject.GetComponent<EnemyFSM>();

        //    move = gameObject.GetComponent<EnemyMovement>();

        //    fsm.SetState(EnemyFSM.EnemyState.patrolState);
        //}
        //else
        //{
        //    return;
        //}
    }

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
            player = FindNearestPlayer(); 

            fsm.UpdateFSM();

            move.UpdateMovement();

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < fsm.GetChaseRange())
            {
                fsm.SetState(EnemyFSM.EnemyState.chaseState);

                if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= fsm.GetAttackRange())
                {
                    fsm.SetState(EnemyFSM.EnemyState.attackState);
                }
            }
        }
    }

    private GameObject FindNearestPlayer() 
    {
        GameObject nearestPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        float oldSize = Int32.MaxValue;
        float size = 0;
        
        foreach (GameObject play in players) 
        {
            size =  Vector3.Distance(gameObject.transform.position, play.transform.position);
            if (size < oldSize) 
            {
                oldSize = size;
                nearestPlayer = play;
            }
        }

        return nearestPlayer;
    }
}
