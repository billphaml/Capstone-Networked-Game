using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanAttack : MonoBehaviour
{

    public EnemyFSM state;
    public EnemyFSM.EnemyState enemyState;
    public GameObject ExplodeSample = null;

    private void Start()
    {
        state = gameObject.GetComponent<EnemyFSM>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyState = state.GetEnemyState();

        if (enemyState == EnemyFSM.EnemyState.attackState)
        {
            ProcessExplode();
        }
    }

    /*    if player is in range
        spawns "explosion" object
        and self-destructs*/
    private void ProcessExplode()
    {
        //GameObject b = GameObject.Instantiate(ExplodeSample) as GameObject;
        //b.transform.position = transform.position;
        //FloorGameController.numberOfEnemies--;
        //Destroy(gameObject);
    }
}
