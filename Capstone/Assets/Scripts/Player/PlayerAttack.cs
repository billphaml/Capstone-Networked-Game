/******************************************************************************
 * 
 *****************************************************************************/

using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using Photon.Realtime;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    
    [SerializeField] float meleeRange;
    [SerializeField] float rangeRange;
    
    public Transform player;
    [SerializeField] public PlayerController thePlayer;
    [SerializeField] LayerMask whatIsEnemy;
    [SerializeField] private GameObject attackBox;
    [SerializeField] private GameObject rangeBox;

    private PlayerActor.attackType weaponType; //1) Sword 2) Bow 3) Spells?
    private float damage;
    private float attackSpeed; //not developed yet

    private List<Collider2D> alreadyDamagedEnemies = new List<Collider2D>();

    NetworkVariableBool attack = new NetworkVariableBool(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, false);

    // Start is called before the first frame update
    private void Start()
    {
        if (IsLocalPlayer)
        {
            thePlayer = gameObject.GetComponent<PlayerController>();
            weaponType = thePlayer.stats.thePlayer.getAttackType();
            damage = thePlayer.stats.thePlayer.playerAttack;
        }
    }

    // Update is called once per frame
    public void UpdateAttack()
    {
        attack.Value = Input.GetMouseButtonDown(0);
        if (attack.Value == true)
        {
            switch (weaponType)
            {
                case Actor.attackType.SWORD:
                    MeleeAttackServerRpc();
                    break;
                case Actor.attackType.GREATSWORD:
                    MeleeAttackServerRpc();
                    break;
                case Actor.attackType.DAGGER:
                    MeleeAttackServerRpc();
                    break;
                case Actor.attackType.BOW:
                    RangeAttackServerRpc();
                    break;
                case Actor.attackType.MAGIC:
                    RangeAttackServerRpc();
                    break;
            }
               
        }
    }

    /// <summary>
    /// Melee Hit detectuion using COllider2D
    /// </summary>
    /// <param name="rpcParams"></param>
    [ServerRpc]
    void MeleeAttackServerRpc(ServerRpcParams rpcParams = default)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackBox.transform.position, new Vector2(meleeRange, meleeRange), 0, whatIsEnemy);
        foreach (var currentEnemy in enemiesToDamage)
        {
            // Skip if you already damaged this enemy
            if (alreadyDamagedEnemies.Contains(currentEnemy)) continue;

            currentEnemy.GetComponent<EnemyDamageable>().DealDamage(damage);
            Debug.Log("hit" + currentEnemy);

            // Add the damaged enemy to the list
            alreadyDamagedEnemies.Add(currentEnemy);
        }
        Debug.Log(alreadyDamagedEnemies);
        alreadyDamagedEnemies.Clear();
    }

    /// <summary>
    /// Ranged Hit detection using Raycasting
    /// Also responsible for projectile display
    /// </summary>
    [ServerRpc]
    void RangeAttackServerRpc(ServerRpcParams rpcParams = default)
    {
        //Ray ray = new Ray(shootParticleSystem.transform.position, shootParticleSystem.transform.forward);
        //if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        //{
            //hit.collider.GetComponent<EnemyDamageable>().DealDamage(damage);
            //Debug.Log("hit" + hit.collider);
        //}
        //Transform arrowTransform = Instantiate(Projectile, attackBox.transform.position, Quaternion.identity);
        //Vector3 trajectory = (attackBox.transform.position - rangeBox.transform.position);
    }

    /// <summary>
    /// Honestly this stuff dosn't work for me. I think it draws outlines or somthing in the prefab veiw?
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackBox.transform.position, new Vector3(meleeRange, meleeRange, 0f));
    }
}
