using UnityEngine;

public class ItemRegister : MonoBehaviour
{
    [SerializeField] private SpawnDemoManager temp = null;

    private float nextDeletionTime = 0f;

    private float deletionDelayTime = 0.4f;

    void Start()
    {
        Debug.Log("Spawned");
        temp = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnDemoManager>();
        temp.spawnCount++;
        nextDeletionTime = Time.time + deletionDelayTime;
    }

    private void FixedUpdate()
    {
        if (nextDeletionTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}
