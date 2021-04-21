using System.Runtime.InteropServices;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class PlayerDamage : NetworkBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public ParticleSystem shootParticleSystem;
    [SerializeField] float meleeRange;
    [SerializeField] float rangeRange;
    [SerializeField] float damage;
    [SerializeField] private Transform Projectile;
    [SerializeField] LayerMask whatIsEnemy;
    public Transform player;
    [SerializeField] private GameObject attackBox;
    [SerializeField] private GameObject rangeBox;
    [SerializeField] private int weaponType; //1) Sword 2) Bow 3) Spells?

    private List<Collider2D> alreadyDamagedEnemies = new List<Collider2D>();

    NetworkVariableBool attack = new NetworkVariableBool(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, false);
    
    /// <summary>
    /// Empty
    /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            attack.Value = Input.GetMouseButtonDown(0);
            if (attack.Value == true)
            {
                if (weaponType == 1)
                {
                    SwingServerRpc();
                }
                else if (weaponType == 2)
                {
                    ShootServerRpc();
                }
               
            }
        }
    }

    /// <summary>
    /// Melee Hit detectuion using COllider2D
    /// </summary>
    /// <param name="rpcParams"></param>
    [ServerRpc]
    void SwingServerRpc(ServerRpcParams rpcParams = default)
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
    void ShootServerRpc(ServerRpcParams rpcParams = default)
    {
        Ray ray = new Ray(shootParticleSystem.transform.position, shootParticleSystem.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            hit.collider.GetComponent<EnemyDamageable>().DealDamage(damage);
            Debug.Log("hit" + hit.collider);
        }
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
