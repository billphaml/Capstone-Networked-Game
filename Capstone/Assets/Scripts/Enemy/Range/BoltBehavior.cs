using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the ranged enemy's bolt so each bolt knows where to go

public class BoltBehavior : MonoBehaviour
{
    // All instances of BoltBehavior share this one BoltSystem
    private static BoltSpawnSystem kBoltSystem = null;
    public static void InitializeBoltSystem(BoltSpawnSystem b) { kBoltSystem = b; }

    private GameObject player;

    [SerializeField]
    private float boltSpeed = 2f;

    [SerializeField]
    private float damageAmount = 1f;

    private void Start()
    {
        /*
        // Gets the player's collider and the shot collider and have them ignore each other
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("RangedEnemy").GetComponent<Collider2D>();
        Collider2D shotCollider = gameObject.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(shotCollider, playerCollider);
        */
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // move towards hero's last position
        transform.position += transform.up * (boltSpeed * Time.smoothDeltaTime);
    }

    #region Deletion & Damage Support
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //// deletes the object if it hits the player
        //if (collision.gameObject.tag == "Player")
        //{
        //    // Damage the hero when the projectile hits the player
        //    PlayerBehavior i = player.GetComponent<PlayerBehavior>();
        //    i.DamagePlayer(damageAmount);
        //    DestroyThisBolt(collision.gameObject.name);
        //}
        //// Remove if collide with the wall tilemap
        //else if (collision.gameObject.tag == "Wall")
        //    DestroyThisBolt(collision.gameObject.name);
    }

    private void DestroyThisBolt(string name)
    {
        // Watch out!! a collision with overlap objects (e.g., two objects at the same location 
        // will result in two OnTriggerEntger2D() calls!!
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);  // set inactive!
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Calling Bolt Destroy on a destroyed bolt: " + name);
        }
    }
    #endregion
}
