using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    public EnemyFSM state;
    public EnemyFSM.EnemyState enemyState;
    public NavMeshAgent agent;

    /// <summary>
    /// Make sure evade is less than attack range.
    /// </summary>
    public float evadeDistance = 4;

    private int randomDir = 0;
    private Vector3 newDest = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        state = gameObject.GetComponent<EnemyFSM>();;
        agent = gameObject.GetComponent<NavMeshAgent>();
        randomDir = Random.Range(1, 4);
        newDest = RandomVector(-20, 20);
    }

    // Update is called once per frame
    void Update()
    {
        enemyState = state.GetEnemyState();
        switch (enemyState)
        {
            case EnemyFSM.EnemyState.patrolState:
                patroleMovement();
                break;
            case EnemyFSM.EnemyState.idleState:
                agent.SetDestination(transform.position);
                break;
            case EnemyFSM.EnemyState.chaseState:
                newDest = player.transform.position;
                agent.SetDestination(newDest);
                break;
            case EnemyFSM.EnemyState.attackState:
                if (gameObject.tag.Equals("MeleeEnemy"))
                {
                    agent.SetDestination(transform.position);
                }
                 
                if (gameObject.tag.Equals("RangedEnemy"))
                {
                    agent.SetDestination(player.transform.position);
                }

                if (gameObject.tag.Equals("Brute"))
                {
                    agent.SetDestination(transform.position);
                }

                if (gameObject.tag.Equals("Adware"))
                {
                    AdwareMove();
                }

                break;
        }
    }
    
    /// <summary>
    /// Move adware to attack player, kite adware away if too close to player.
    /// Make sure that evadeDistance range is lower than attack range
    /// </summary>
    private void AdwareMove()
    {
        // If player enters evade range then move else stay
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < (gameObject.GetComponent<EnemyFSM>().getAttackRange() - evadeDistance))
        {
            // Grab the direction to move away from
            var newVec = gameObject.transform.position - player.transform.position;

            // Calculate the new position to move to
            newVec = new Vector3((newVec.x * 2), (newVec.y * 2), newVec.z) + gameObject.transform.position;
            agent.SetDestination(newVec);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

        //if (Vector3.Distance(gameObject.transform.position, player.transform.position) < gameObject.GetComponent<EnemyFSM>().getAttackRange())
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
    /// facilitates the random movement and biased movement towards the player
    /// </summary>
    void patroleMovement() 
    {
        if (state.isExecutingState())
        {
            randomDir = Random.Range(1, 4);
            //newDest = RandomVector(-20, 20);

            if (RandomPoint(transform.position, 10.0f, out newDest))
            {
                //Debug.DrawLine(newDest, Vector3.up, Color.blue, 1.0f);
            }
        }

        if (randomDir > 2)
        {
            Debug.DrawLine(newDest, gameObject.transform.position, Color.blue, 1.0f);
            agent.SetDestination(newDest);
        }
        else
        {
            Debug.DrawLine(player.transform.position, gameObject.transform.position, Color.red, 1.0f);
            agent.SetDestination(player.transform.position);
        }
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

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
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

    /// <summary>
    /// Return the new destination the enemy is navigating to.
    /// </summary>
    /// <returns></returns>
    public Vector3 getNewDest()
    {
        return newDest;
    }
}
