using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * See BoltBehavior: works very similarly
 * For now just having a lot of duplicate code
 *
 */
public class AdBoltBehavior : MonoBehaviour
{
    // All instances of BoltBehavior share this one BoltSystem
    private static AdBoltSpawnSystem kAdSystem = null;
    public static void InitializeAdSystem(AdBoltSpawnSystem a) { kAdSystem = a; }

    private GameObject player;

    [SerializeField]
    private float boltSpeed = 2f;

    [SerializeField]
    private float damageAmount = 1f;

    private float kSizeIncrement = 0.0002f;
    private float scale = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // move towards hero's last position
        transform.position += transform.right * (boltSpeed * Time.smoothDeltaTime);
        // gradually increase in size
        transform.localScale = new Vector3(scale, scale, 0f);
        scale = scale + kSizeIncrement;
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
