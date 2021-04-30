/******************************************************************************
 * Attach to a GameObject with a trigger rigidbody to start spawning waves
 * in a room where the player collides with the GameObject.
 * 
 * Authors: Alicia T, Bill P, Hans W, Hamza S
 *****************************************************************************/

//#undef DEBUG
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class WaveTrigger : NetworkBehaviour
{
    /// <summary>
    /// The room that the wave trigger is location in/coressponds with.
    /// </summary> 
    [SerializeField]
    private GameObject room = null;

    //[SerializeField]
    //private GameObject[] doors;

    private void Start()
    {
        // Checks if a room was assigned to before invoking spawning
        if (room == null)
        {
            Debug.LogError("Unable to get room for spawning.");
        }
    }

    /// <summary>
    /// Starts the waves for the room.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && IsHost)
        {
#if (DEBUG)
            Debug.Log("Triggered by player/object");
#endif
            room.GetComponent<EnemySpawner>().StartWaves();

            //for (int i = 0; i < doors.Length; i++)
            //{
            //    doors[i].GetComponent<BoxCollider2D>().enabled = true;
            //    doors[i].GetComponent<SpriteRenderer>().enabled = true;
            //}

            // Lock the room (Future item)

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player" && NetworkManager.Singleton.IsConnectedClient)
        {
            SubmitSpawnRequestServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SubmitSpawnRequestServerRpc(ServerRpcParams rpcParams = default) 
    {
#if (DEBUG)
        Debug.Log("Triggered by player/object");
#endif
        room.GetComponent<EnemySpawner>().StartWaves();

        //for (int i = 0; i < doors.Length; i++)
        //{
        //    doors[i].GetComponent<BoxCollider2D>().enabled = true;
        //    doors[i].GetComponent<SpriteRenderer>().enabled = true;
        //}

        // Lock the room (Future item)

        Destroy(gameObject);
    }
}
