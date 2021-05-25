
using UnityEngine;
using MLAPI;

public class BoltSpawnSystem : MonoBehaviour
{

    // Spawning support
    private GameObject boltSample = null;

    // Cooldown between shots
    [SerializeField]
    private float fireRate = 1f;
    private float kSpawnBoltAt = 0f;

    // Start is called before the first frame update
    void Start()
    {
        boltSample = Resources.Load<GameObject>("Prefabs/Enemies/Bolts/Ranged Bolt") as GameObject;

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

    public void SpawnABolt(Vector3 p, Vector3 dir)
    {
        Debug.Assert(CanSpawn());
        GameObject b = GameObject.Instantiate(boltSample) as GameObject;
        b.GetComponent<NetworkObject>().Spawn();
        b.transform.position = p;
        b.transform.up = dir; // changed this from up to right 
        kSpawnBoltAt = Time.realtimeSinceStartup;
    }
    #endregion
}
