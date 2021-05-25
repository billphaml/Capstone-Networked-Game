using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public EnemyController controller;
    public EnemyFSM state;
    public EnemyFSM.EnemyState enemyState;
    //private GameObject player;
    private BoltSpawnSystem kBoltSystem;

    private void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
        kBoltSystem = gameObject.GetComponent<BoltSpawnSystem>();
        state = gameObject.GetComponent<EnemyFSM>();
       // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemyState = state.GetEnemyState();

        if (enemyState == EnemyFSM.EnemyState.attackState)
        {
            state.SetIsAttacking(true);
            ProcessAttack();
        }
        else
        {
            state.SetIsAttacking(false);
        }
    }

    // Function for attack
    // checks if in range, then checks if it's on cooldown, then spawns attack
    private void ProcessAttack()
    {
        if (kBoltSystem.CanSpawn())
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 0.0f);
            kBoltSystem.SpawnABolt(newPos, (controller.player.transform.position - newPos));
        }
    }
}
