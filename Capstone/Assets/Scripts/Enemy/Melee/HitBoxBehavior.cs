using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{
    private GameObject player;

    public GameObject hitBox = null;
    public float hitBoxRadius = 0.5f;
    public bool hitBoxEnabled = false;
    public LayerMask playerMask;

    private float kHitBoxActiveTime = 60f;
    private float timer = 0f;

    // the enemies that use this should give
    // it's corresponding damage amount when it's called 
    private float damageAmount = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update()
    {
        // start timer once the hit box is enabled
        if(hitBoxEnabled)
            timer++;
        ProcessTime();
    }

    // hit box only lasts for 1 second
    private void ProcessTime()
    {
        if(timer > kHitBoxActiveTime)
        {
            hitBoxEnabled = false;
            timer = 0f;
        }
    }

    // only does anything if the hit box is enabled
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision Detected");
    //    if (hitBoxEnabled)
    //    {
    //        Debug.Log("Hit box on");
    //        if (collision.gameObject.tag == "Player")
    //        {
    //            Debug.Log("Player hit");
    //            // Damage the player when the player is in the hit box
    //            // and the hit box is enabled
    //            PlayerHit();
    //        }
    //    }
    //}

    // if the hit box touches the player when it's enabled
    // player takes damage
    public void PlayerHit(string enemyType)
    {
        //Collider2D[] hitObjects = Physics2D.OverlapCircleAll(hitBox.transform.position, hitBoxRadius, playerMask);
        //Debug.Log("Trying to attack");
        //if (hitObjects.Length > 0)
        //{
        //    PlayerBehavior i = player.GetComponent<PlayerBehavior>();

        //    if (enemyType.Equals("Brute"))
        //    {
        //        i.DamagePlayer(damageAmount);
        //        i.DamagePlayer(damageAmount);
        //        i.StunPlayer();
        //    }

        //    if (enemyType.Equals("MeleeEnemy"))
        //    {
        //        i.DamagePlayer(damageAmount);
        //    }

        //    hitBoxEnabled = false;
        //    Debug.Log("Damaged Player");
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.transform.position, hitBoxRadius);
    }

    // enables hit box
    public void EnableHitBox(float d)
    {
        hitBoxEnabled = true;
        damageAmount = d;
    }
}
