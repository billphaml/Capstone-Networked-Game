using UnityEngine;

public class AdwareAttack : MonoBehaviour
{
    public EnemyFSM state;
    public EnemyFSM.EnemyState enemyState;
    private GameObject player;
    private AdBoltSpawnSystem kBoltSystem;

    private void Start()
    {
        kBoltSystem = gameObject.GetComponent<AdBoltSpawnSystem>();
        state = gameObject.GetComponent<EnemyFSM>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemyState = state.GetEnemyState();

        if (enemyState == EnemyFSM.EnemyState.attackState)
        {
            ProcessAttack();
        }
    }

    // Function for attack
    // checks if in range, then checks if it's on cooldown, then spawns attack
    private void ProcessAttack()
    {
        if (kBoltSystem.CanSpawn())
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 0.0f);
            kBoltSystem.SpawnABolt(newPos, (player.transform.position - newPos));
        }
    }
}
