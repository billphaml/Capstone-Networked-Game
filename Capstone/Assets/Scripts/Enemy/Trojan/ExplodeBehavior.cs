using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a script for the explosion sprite
// Can add the trojan damage to other enemies 
// to this script eventually
public class ExplodeBehavior : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float damageAmount = 1f;
    [SerializeField]
    private float pushFactor = 300000;

    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update()
    {
        // damage enemies if there are enemies to be damaged
        if(enemies.Count > 0)
            DamageAllEnemies();
    }

    // Animation Event calls delete when the
    // animation ends
    public void Delete()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //// checks for if the spawned explosion hits the player
        //if (col.gameObject.tag == "Player")
        //{
        //    // Damage the hero when the explosion hits the player
        //    PlayerBehavior i = player.GetComponent<PlayerBehavior>();
        //    i.DamagePlayer(damageAmount);
        //    Rigidbody2D force = i.GetComponent<Rigidbody2D>();
        //    Vector3 pushBack = (i.transform.position - gameObject.transform.position).normalized;
        //    //Debug.Log(pushBack);
        //    force.AddForce(pushBack * (pushFactor * 0.5f));
        //}

        //// checks for if the spawned explosion hits any enemies
        //else if (col.gameObject.tag == "MeleeEnemy" || col.gameObject.tag == "RangedEnemy" ||
        //    col.gameObject.tag == "Brute" || col.gameObject.tag == "Adware" || col.gameObject.tag == "Trojan")
        //{
        //    // add the object collided with to the list of enemies to damage
        //    enemies.Add(col.gameObject);
        //}

        //Delete();
    }

    // Damages all the enemies that are in the list of enemies to damage
    private void DamageAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            TakeDamage(enemy, damageAmount);
        }
        enemies.Clear();
    }

    // Function that detects what type of enemy it is and
    // calls it's respective trojan damage function that
    // handles it's health 
    // assumes all enemies have their respective behavior components
    // and that all enemies have the TrojanDamage function
    public void TakeDamage(GameObject enemy, float dmg)
    {
        if(enemy.tag == "MeleeEnemy")
        {
            //Debug.LogError("Readd reference to meleeInteraction");
            EnemyInteraction m = enemy.GetComponent<EnemyInteraction>();
            m.TrojanDamage(damageAmount);
            Vector3 pushBack = enemy.transform.position - gameObject.transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(pushBack * pushFactor);

        }
        else if(enemy.tag == "RangedEnemy")
        {
            //Debug.LogError("Readd reference to meleeInteraction");
            EnemyInteraction m = enemy.GetComponent<EnemyInteraction>();
            m.TrojanDamage(damageAmount);
            Vector3 pushBack = enemy.transform.position - gameObject.transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(pushBack * pushFactor);
        }
        else if(enemy.tag == "Brute")
        {
            //Debug.LogError("Readd reference to meleeInteraction");
            EnemyInteraction m = enemy.GetComponent<EnemyInteraction>();
            m.TrojanDamage(damageAmount);
        }
        else if(enemy.tag == "Adware")
        {
            //Debug.LogError("Readd reference to meleeInteraction");
            EnemyInteraction m = enemy.GetComponent<EnemyInteraction>();
            m.TrojanDamage(damageAmount);
            Vector3 pushBack = enemy.transform.position - gameObject.transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(pushBack * pushFactor);
        }
        else if(enemy.tag == "Trojan")
        {
            //Debug.LogError("Readd reference to meleeInteraction");
            EnemyInteraction m = enemy.GetComponent<EnemyInteraction>();
            m.TrojanDamage(damageAmount);
            Vector3 pushBack = enemy.transform.position - gameObject.transform.position;
            enemy.GetComponent<Rigidbody2D>().AddForce(pushBack * pushFactor);
        }    
    }
}
