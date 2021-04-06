//#undef DEBUG

using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        patrolState,
        chaseState,
        idleState,
        attackState,
    };

    ///<summary> private variables for our enemy values change depending on type </summary>
    
    /// <summary> Default Timer </summary> 
    [SerializeField]
    private float kTimer = 480f; // 8 sec
    ///<summary> Num seconds for idle state  </summary>
    [SerializeField]
    private float kIdleTimer = 150f; // 2.5 sec
    ///<summary> Num seconds for attack state </summary>
    [SerializeField]
    private float kAttackTimer = 60f; // 1 sec
    ///<summary> chase range </summary>
    [SerializeField]
    private float kChaseRange = 5f;  // 5 units
    ///<summary> effective attack range </summary>
    [SerializeField]
    private float kAttackRange = 1.5f; // 1 and a half units

    private bool isAttacking = false;

    private GameObject player;

    private int timerTick = 0;

    /// <summary> The current state of the enemy </summary>
    public EnemyState mState;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //enemy FMS basic behavior for the enemy cycles through patrol and indle until player enters effective range
    public void meleeMovementFSM() 
    {
        switch (mState) 
        {
            case EnemyState.patrolState:
                patrolBehavior();
                break;
            case EnemyState.idleState:
                idleBehavior();
                break;
            case EnemyState.chaseState:
                chaseBehavior();
                break;
            case EnemyState.attackState:
                attackBehavior();
                break;

        }
    }

    // Randomly goes in different directions but is biased towards moving to the player
    void patrolBehavior()
    {
        if (timerTick > kTimer)
        {
            mState = EnemyState.idleState;
            timerTick = 0;
        }
        else
        {
            #if (DEBUG)
                //Debug.Log("Patrol");
            #endif
            timerTick++;
        }

    }

    //basic idle for the enemy they have an interesting animation also allows the player to get some cheep shots in
    void idleBehavior() 
    {
        if (timerTick > kIdleTimer)
        {
            mState = EnemyState.patrolState;
            timerTick = 0;
        }
        else 
        {
            #if (DEBUG)
                //Debug.Log("Idle");
            #endif

            //flip sprite left and right some sort of animation enemy is static
            timerTick++;
        }
    }
    
    //enemy chases the player nothing too fancy but will transition to attack phase after chasing for 5 secs
    void chaseBehavior()
    {
        if (timerTick > kTimer)
        {
            mState = EnemyState.attackState;
            timerTick = 0;
        }
        else 
        {
            #if (DEBUG)
                //Debug.Log("Chase");
            #endif
            timerTick++;

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > kChaseRange &&
                !isAttacking)
            {
                mState = EnemyState.patrolState;
            }
        }
    }

    //attack behavior just calls the attack for the enemy sometimes set to attack state if in attack range (melee only)
    void attackBehavior() 
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

    public float getAttackRange() 
    {
        return kAttackRange;
    }

    public float getChaseRange() 
    {
        return kChaseRange;
    }

    public void setState(EnemyState state) 
    {
        mState = state;
    }

    public bool isExecutingState()
    {
        //Debug.Log(timerTick > kTimer);
        return timerTick > kTimer;
    }

    public bool isExecutingAttackState()
    {
        //Debug.Log(timerTick > kAttackTimer);
        return timerTick > kAttackTimer;
    }

    public Vector3 getPlayerVector()
    {
        return player.transform.position;
    }

    public void setAttacking(bool attack)
    {
        isAttacking = attack;
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }
}
