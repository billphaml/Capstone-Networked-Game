/******************************************************************************
 * Damageable script that allows objects with a health script to take damage.
 * Objects attacking this object should call this object's DealDamage() 
 * function to do damage to this object. Never change health directly, add
 * functions to this class that make rpc on your behalf to affect health.
 * 
 * TODO:
 * - 
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using Unity.VisualScripting;
using System.Collections;
using UnityEditor;

public class EnemyDamageable : NetworkBehaviour
{
    /// <summary>
    /// Reference to health component. Make sure one exists on this object.
    /// </summary>
    private EnemyHealth health = null;
    public GameObject spriteObject = null;
    private SpriteRenderer sprite;
 

    /// <summary>
    /// Similar to awake but for occurs when all clients are synced.
    /// </summary>
    public virtual void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
        sprite = spriteObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Calls a rpc in health to remove health equal to the passed in amount
    /// of damage.
    /// </summary>
    /// <param name="damageToDeal"></param>
    public virtual void DealDamage(float damageToDeal)
    {
        health.RemoveHealthServerRpc(damageToDeal);
        StartCoroutine(flash());        
    }

    public IEnumerator flash() 
    {
        for (int i = 0; i <= 5; i++)
        {  
            sprite.enabled =  false;
            yield return new WaitForSeconds(.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        sprite.enabled = true;
        yield return null;
    }
}
