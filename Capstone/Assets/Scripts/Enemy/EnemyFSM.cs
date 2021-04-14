/******************************************************************************
 * Controls enemy state.
 *****************************************************************************/

// Uncomment for debug mode
//#define DEBUG
// Uncomment for normal mode
#undef DEBUG

using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    /// <summary>
    /// Reference to enemy controller to get other references i.e. player.
    /// </summary>
    private EnemyController ec = default;

    public enum EnemyState
    {
        patrolState,
        chaseState,
        idleState,
        attackState
    };

    /// <summary>
    /// The current state of the enemy.
    /// </summary>
    [SerializeField] private EnemyState mState;

    /// <summary>
    /// Duration in seconds for patrol state.
    /// </summary>
    [SerializeField] private float kPatrolTimer = 480f;

    /// <summary>
    /// Duration in seconds for chase state.
    /// </summary>
    [SerializeField] private float kChaseTimer = 480f;

    /// <summary>
    /// Duration in seconds for idle state.
    /// </summary>
    [SerializeField] private float kIdleTimer = 150f;

    /// <summary>
    /// Duration in seconds for attack state.
    /// </summary>
    [SerializeField] private float kAttackTimer = 60f;

    /// <summary>
    /// Chase range in units.
    /// </summary>
    [SerializeField] private float kChaseRange = 5f;

    /// <summary>
    /// Attack range in units.
    /// </summary>
    [SerializeField] private float kAttackRange = 1.5f;

    /// <summary>
    /// Timer for state changes.
    /// </summary>
    [SerializeField] private int timerTick = 0;

    /// <summary>
    /// Check if in attack animation.
    /// </summary>
    private bool isAttacking = false;

    public bool isPatroling = false;

    private void Start()
    {
        // Could probably set this to happen only on server, not sure if references waste memory
        ec = gameObject.GetComponent<EnemyController>();
    }

    /// <summary>
    /// Enemy FSM basic behavior where the enemy cycles through patrol and idle
    /// until player enters effective range.
    /// </summary>
    public void UpdateFSM() 
    {
        switch (mState) 
        {
            case EnemyState.patrolState:
                PatrolBehavior();
                break;
            case EnemyState.idleState:
                IdleBehavior();
                break;
            case EnemyState.chaseState:
                ChaseBehavior();
                break;
            case EnemyState.attackState:
                AttackBehavior();
                break;
        }
    }

    /// <summary>
    /// Randomly goes in different directions but is biased towards moving to
    /// the player.
    /// </summary>
    private void PatrolBehavior()
    {
        if (timerTick > kPatrolTimer)
        {
            mState = EnemyState.idleState;
            timerTick = 0;
            isPatroling = false;
        }
        else
        {
#if (DEBUG)
            Debug.Log("State: Patrol");
#endif
            timerTick++;
        }
    }

    /// <summary>
    /// Basic idle for the enemy they have an interesting animation also allows
    /// the player to get some cheep shots in.
    /// </summary>
    private void IdleBehavior()
    {
        if (timerTick > kIdleTimer)
        {
            mState = EnemyState.patrolState;
            timerTick = 0;
        }
        else 
        {
#if (DEBUG)
            Debug.Log("Idle");
#endif
            // Flip sprite left and right some sort of animation enemy is static
            timerTick++;
        }
    }

    /// <summary>
    /// Enemy chases the player nothing too fancy but will transition to attack
    /// phase after chasing for 5 secs.
    /// </summary>
    private void ChaseBehavior()
    {
        if (timerTick > kChaseTimer)
        {
            mState = EnemyState.attackState;
            timerTick = 0;
        }
        else 
        {
#if (DEBUG)
            Debug.Log("Chase");
#endif
            timerTick++;

            if (Vector3.Distance(gameObject.transform.position, ec.player.transform.position) >
                kChaseRange && !isAttacking)
            {
                mState = EnemyState.patrolState;
            }
        }
    }

    /// <summary>
    /// Attack behavior just calls the attack for the enemy sometimes set to 
    /// attack state if in attack range (melee only).
    /// </summary>
    private void AttackBehavior() 
    {
        if (timerTick > kAttackTimer)
        {
            mState = EnemyState.chaseState;
            timerTick = 0;
        }
        else 
        {
#if (DEBUG)
            //Debug.Log("Attack");
#endif
            timerTick++;
        }
    }

    public EnemyState GetEnemyState() 
    {
        return mState;
    }

    public void SetState(EnemyState state)
    {
        mState = state;
    }

    public float GetAttackRange() 
    {
        return kAttackRange;
    }

    public float GetChaseRange()
    {
        return kChaseRange;
    }

    public bool IsExecutingPatrolState()
    {
        //return timerTick > kPatrolTimer;
        return isPatroling;
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}
