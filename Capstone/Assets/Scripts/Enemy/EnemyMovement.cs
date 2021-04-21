/******************************************************************************
 * Controls enemy movement.
 * 
 * TODO:
 * - Uncomment player dest set once player search is implemented
 *****************************************************************************/

using UnityEngine;
using UnityEngine.AI;
using MLAPI;

public class EnemyMovement : NetworkBehaviour
{
    /// <summary>
    /// Reference to enemy controller to get other references i.e. player.
    /// </summary>
    private EnemyController ec = default;

    /// <summary>
    /// Reference to NavMeshAgent.
    /// </summary>
    private NavMeshAgent agent = default;

    /// <summary>
    /// Distance that certain enemies will stay away from the player.
    /// Note: Make sure evade is less than attack range.
    /// </summary>
    //[SerializeField] private float evadeDistance = 4;

    /// <summary>
    /// Variable to affect enemy bias in moving towards the player when
    /// randomly patroling. Values 3 and higher decrease player bias.
    /// This variable gets changed at runtime, if you want to decrease player
    /// bias, edit the random number range in the PatrolMovement function.
    /// This should be updated later to support range changing.
    /// </summary>
    [SerializeField] private int playerBias = 3;

    /// <summary>
    /// Next destination enemy will navigate to.
    /// </summary>
    private Vector3 newDest = default;

    private void Start()
    {
        // Could probably set this to happen only on server, not sure if references waste memory
        ec = gameObject.GetComponent<EnemyController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public override void NetworkStart()
    {
        base.NetworkStart();
        ec = gameObject.GetComponent<EnemyController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    //
    /// <summary>
    /// Updates position enemy should move to.
    /// </summary>
    public void UpdateMovement()
    {
        switch (ec.fsm.GetEnemyState())
        {
            case EnemyFSM.EnemyState.patrolState:
                PatrolMovement();
                break;
            case EnemyFSM.EnemyState.idleState:
                //agent.SetDestination(gameObject.transform.position);
                break;
            case EnemyFSM.EnemyState.chaseState:
                newDest = ec.player.transform.position;
                agent.SetDestination(newDest);
                break;
            case EnemyFSM.EnemyState.attackState:
                if (gameObject.tag.Equals("MeleeEnemy"))
                {
                    agent.SetDestination(transform.position);
                }
                
                if (gameObject.tag.Equals("RangedEnemy"))
                {
                    agent.SetDestination(ec.player.transform.position);
                }

                // Update as Evasion enemy? Disabled for alpha build
                //if (gameObject.tag.Equals("Adware"))
                //{
                //    AdwareMove();
                //}

                break;
        }
    }
    
    /// <summary>
    /// Move adware to attack player, kite adware away if too close to player.
    /// Make sure that evadeDistance range is lower than attack range
    /// </summary>
    private void AdwareMove()
    {
        //// If player enters evade range then move else stay
        //if (Vector3.Distance(gameObject.transform.position, player.transform.position) <
        //    (gameObject.GetComponent<EnemyFSM>().GetAttackRange() - evadeDistance))
        //{
        //    // Grab the direction to move away from
        //    var newVec = gameObject.transform.position - player.transform.position;

        //    // Calculate the new position to move to
        //    newVec = new Vector3((newVec.x * 2), (newVec.y * 2), newVec.z) + gameObject.transform.position;
        //    agent.SetDestination(newVec);
        //}
        //else
        //{
        //    agent.SetDestination(transform.position);
        //}

        //if (Vector3.Distance(gameObject.transform.position, player.transform.position) <
        //    gameObject.GetComponent<EnemyFSM>().getAttackRange())
        //{
        //    if (state.isExecutingState())
        //    {
        //        // Get the player position and find a point 10 units away from the player to kite to
        //        if (RandomPoint(player.transform.position, 11f, out newDest))
        //        {
        //            //Debug.DrawLine(newDest, Vector3.up, Color.blue, 1.0f);
        //        }
        //    }

        //    agent.SetDestination(newDest);
        //}
    }

    /// <summary>
    /// Facilitates the random movement and biased movement towards the player.
    /// </summary>
    private void PatrolMovement() 
    {
        if (!ec.fsm.IsExecutingPatrolState())
        {
            playerBias = Random.Range(1, 4);

            if (RandomPoint(transform.position, 10.0f, out newDest))
            {
                Debug.DrawLine(newDest, Vector3.up, Color.yellow, 1.0f);
            }

            ec.fsm.isPatroling = true;
        }

        Debug.DrawLine(newDest, gameObject.transform.position, Color.blue, 1.0f);
        agent.SetDestination(newDest);

        //if (playerBias > 2)
        //{
            
        //}
        //else
        //{
        //    //Debug.DrawLine(ec.player.transform.position, gameObject.transform.position, Color.red, 1.0f);
        //    //agent.SetDestination(ec.player.transform.position);
        //}
    }

    /// <summary>
    /// Helper method for patrolMovement()
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns> random position on map </returns>
    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
    }

    /// <summary>
    /// Returns a random position around a specified vector3.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="range"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;

        return false;
    }
}
