using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * See BoltSpawnSystem: works very similarly
 * For now just having a lot of duplicate code
 * 
 * Also controls the ad window spawn
 * 
 */
public class AdBoltSpawnSystem : MonoBehaviour
{
    // Spawning support
    private GameObject kBoltSample = null;

    // Cooldown between shots
    [SerializeField]
    private float fireRate = 1f;
    private float kSpawnBoltAt = 0f;

    // Start is called before the first frame update
    void Start()
    {
        kBoltSample = Resources.Load<GameObject>("Prefabs/Enemies/Bolts/AdBolt") as GameObject;

        kSpawnBoltAt = Time.realtimeSinceStartup - fireRate; // assume one was shot
    }

    // Code from MP4
    #region Spawning support
    public bool CanSpawn()
    {
        return TimeTillNext() <= 0f;
    }

    public float TimeTillNext()
    {
        float sinceLastBolt = Time.realtimeSinceStartup - kSpawnBoltAt;
        return fireRate - sinceLastBolt;
    }

    // Spawns 3 windows that spread
    public void SpawnABolt(Vector3 p, Vector3 dir)
    {
        // the initial offset
        float offSet = 15;
        // loop for amount of spread shots
        for (int i = 0; i < 3; i++)
        {
            // initialize game object at player location
            GameObject b = GameObject.Instantiate(kBoltSample) as GameObject;
            b.transform.position = p;
            b.transform.up = dir;
            // rotates so the each shot goes at a slightly different angle
            b.transform.Rotate(0f, 0f, offSet);
            offSet = offSet - 15; // decrement the offset by thirty degrees
        }
        kSpawnBoltAt = Time.realtimeSinceStartup;
    }
    #endregion

    public float GetFireRate() { return fireRate; }

}
