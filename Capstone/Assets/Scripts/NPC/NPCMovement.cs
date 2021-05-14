/******************************************************************************
 * Controls npc movement. Includes a delay timer between movement updates to
 * lower resource usage.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using UnityEngine.AI;
using MLAPI;

public class NPCMovement : NetworkBehaviour
{
    /// <summary>
    /// Area that the navmesh look at.
    /// </summary>
    private NavMeshQueryFilter filter;

    /// <summary>
    /// Reference to NavMeshAgent.
    /// </summary>
    private NavMeshAgent agent = null;

    /// <summary>
    /// Next destination enemy will navigate to.
    /// </summary>
    private Vector3 newDest = default;

    private float nextUpdateTime = 0f;

    private float updateDelayTime = 10f;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        filter.areaMask = NavMesh.GetAreaFromName("NPC");
    }

    private void Update()
    {
        if (nextUpdateTime <= Time.time)
        {
            if (IsServer || IsHost)
            {
                bool rand = System.Convert.ToBoolean(Random.Range(0, 2));

                if (rand)
                {
                    Patrol();
                }
                else
                {
                    Patrol();
                    // Idle (do nothing)
                }

                nextUpdateTime = Time.time + updateDelayTime;
            }
        }
    }

    private void Patrol()
    {
        if (RandomPoint(transform.position, 10.0f, out newDest))
        {
            Debug.DrawLine(Vector3.zero, newDest, Color.yellow, 1.0f);
        }

        Debug.DrawLine(gameObject.transform.position, newDest, Color.blue, 1.0f);
        agent.SetDestination(newDest);
    }

    /// <summary>
    /// Returns a random position around a specified origin. Point must exist
    /// on the navmesh to be considered valid.
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
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, filter))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;

        return false;
    }
}
