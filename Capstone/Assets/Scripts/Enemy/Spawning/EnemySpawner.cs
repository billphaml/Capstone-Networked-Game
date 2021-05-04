/******************************************************************************
 * The EnemySpawner class randomly spawns an enemy at a random spawn point.
 * 
 * Authors: Alicia T, Bill P, Hans W, Hamza S
 *****************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using MLAPI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] doors;

    [System.Serializable]
    public struct enemyBP
    {
        public GameObject enemy;
        public int enemyCount;
        public float enemyRate;
    }

    [System.Serializable]
    public struct waveDeets
    {
        public int length;
        public enemyBP[] enemies;
    }

    /// <summary>
    /// Array of enemies to spawn.
    /// </summary>
    public waveDeets[] waves;
    public int waveCount;
    public float waveRate;

    /// <summary>
    /// Time Between enemy spawn.
    /// </summary>
    //public float spawnDelayTime = 3f;

    /// <summary>
    /// Array of valid spawnpoints.
    /// </summary>
    public GameObject[] spawnPoints;

    /// <summary>
    /// Array of valid spawnpoint positions.
    /// </summary>
    private Transform[] spawnPointPositions;

    /// <summary>
    /// Starts a invoke to repeatively spawn enemies.
    /// </summary>
    //public void StartWaves()
    //{
    //    InvokeRepeating("Spawn", spawnDelayTime, spawnDelayTime);
    //}

    void Start()
    {
        spawnPointPositions = new Transform[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointPositions[i] = spawnPoints[i].transform;
        }
    }

    /// <summary>
    /// Cancel the invoke to repeatively spawn enemies. A controller class will
    /// need to call this method.
    /// </summary>
    public void EndWaves()
    {
        CancelInvoke();
    }

    private void Update()
    {
        //if (doneSpawning)
        //{
        //    //for (int i = 0; i < doors.Length; i++)
        //    //{
        //    //    doors[i].GetComponent<BoxCollider2D>().enabled = false;
        //    //    doors[i].GetComponent<SpriteRenderer>().enabled = false;
        //    //}
        //    //for (int i = 0; i < spawnPoints.Length; i++)
        //    //{
        //    //    spawnPoints[i].GetComponent<Animator>().SetBool("delete", true);
        //    //}
            
        //    StartCoroutine(DeletePortals());
        //}
    }

    /// <summary>
    /// Randomly spawns an enemy from a array at a random spawn point
    /// selected from a array.
    /// </summary>
    void Spawn(GameObject currentEnemy)
    {
        int spawnPointIndex = Random.Range(0, spawnPointPositions.Length);

        GameObject go = Instantiate(currentEnemy, spawnPointPositions[spawnPointIndex].position, spawnPointPositions[spawnPointIndex].rotation);
        go.GetComponent<NetworkObject>().Spawn();
    }

    public void StartWaves()
    {
        Debug.Log("Bruh");
        StartCoroutine(SpawnWave());
    }

    private IEnumerator<WaitForSeconds> SpawnWave()
    {
        // Spawn wave 0 with immeditately
        for (int j = 0; j < waves[0].length; j++)
        {
            Debug.Log("waves enemy " + j);
            for (int k = 0; k < waves[0].enemies[j].enemyCount; k++)
            {
                Debug.Log("enemies spawn " + k);
                Spawn(waves[0].enemies[j].enemy);
                //FloorGameController.numberOfEnemies++;
                yield return new WaitForSeconds(waves[0].enemies[j].enemyRate);
            }
        }

        // Spawn subsequent waves with a delay
        for (int a = 1; a < waveCount; a++)
        {
            Debug.Log("wave " + a);
            yield return new WaitForSeconds(waveRate);

            for (int j = 0; j < waves[a].length; j++)
            {
                Debug.Log("waves enemy " + j);
                for (int k = 0; k < waves[a].enemies[j].enemyCount; k++)
                {
                    Debug.Log("enemies spawn " + k);
                    Spawn(waves[a].enemies[j].enemy);
                    //FloorGameController.numberOfEnemies++;
                    yield return new WaitForSeconds(waves[a].enemies[j].enemyRate);
                }
            }
        }
    }

    IEnumerator<WaitForSeconds> DeletePortals()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].GetComponent<SpriteRenderer>().enabled = false;
            spawnPoints[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
        }
    }

}
