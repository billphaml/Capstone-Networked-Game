using System.Runtime.InteropServices;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class PlayerDamage : NetworkBehaviour
{
    [SerializeField] float meleeRange;
    [SerializeField] float rangeRange;
    public Transform player;

    private List<Collider2D> alreadyDamagedEnemies = new List<Collider2D>();

    NetworkVariableBool attack = new NetworkVariableBool(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, false);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            attack.Value = Input.GetMouseButton(0);
            if (attack.Value == true)
            {
                InvokeServerRPC(Swing);
            }
        }
    }


    [ServerRpc RPC]
    void Swing()
    {
        int damage = 10;

        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(player.position, new Vector2(meleeRange, meleeRange), 0, whatIsEnemy);
        foreach (var currentEnemy in enemiesToDamage)
        {

            // Skip if you already damaged this enemy
            if (alreadyDamagedEnemies.Contains(currentEnemy)) continue;

            currentEnemy.GetComponent<HealthSystem>().TakeDamage(damage);

            // Add the damaged enemy to the list
            alreadyDamagedEnemies.Add(currentEnemy);
        }
        alreadyDamagedEnemies.Clear();
        Debug.Log("hit");
    }

}
