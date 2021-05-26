/******************************************************************************
 * Class to animate enemy sprite.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    /// <summary>
    /// Reference to enemy controller to get other references i.e. player.
    /// </summary>
    private EnemyController ec = default;

    [SerializeField]
    private Animator enemyAnimator = null;

    [SerializeField]
    private GameObject enemy = null;
    private EnemyFSM enemyFSM = null;
    public EnemyFSM.EnemyState enemyState;

    private void Start()
    {
        ec = enemy.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemyFSM = enemy.GetComponent<EnemyFSM>();
        }
    }

    public void UpdateAnimation()
    {
        if (enemyFSM != null && enemyAnimator != null)
        {
            if (enemyFSM.GetIsAttacking())
            {
                //Debug.Log(enemyFSM.getIsAttacking());
                enemyAnimator.SetBool("moving", false);
                
                var dist = (ec.transform.position - ec.player.transform.position);
                if (dist.magnitude > 3)
                {
                    enemyAnimator.SetBool("attacking", false);
                }
                else
                {
                    enemyAnimator.SetBool("attacking", true);
                }

                if (enemy.tag == "RangedEnemy")
                {
                    var newVec = (ec.transform.position - ec.player.transform.position).normalized;
                    //Debug.Log(newVec);
                    
                    enemyAnimator.SetFloat("horizontal", newVec.x);
                    enemyAnimator.SetFloat("vertical", newVec.y);

                    enemyAnimator.SetBool("moving", true);
                }
            }
            else
            {
                var dist = (ec.transform.position - ec.player.transform.position);

                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack") || dist.magnitude > 3)
                {
                    //Debug.Log(enemyFSM.getIsAttacking());
                    enemyAnimator.SetBool("attacking", false);

                    var newVec = (ec.transform.position - ec.player.transform.position).normalized;
                    //Debug.Log(newVec);

                    enemyAnimator.SetFloat("horizontal", newVec.x);
                    enemyAnimator.SetFloat("vertical", newVec.y);

                    if (newVec.x >= 0)
                    {
                        gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
                    }
                    if (newVec.x < 0)
                    {
                        gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                    }

                    if (enemyFSM.GetEnemyState() == EnemyFSM.EnemyState.patrolState || enemyFSM.GetEnemyState() == EnemyFSM.EnemyState.chaseState)
                    {
                        enemyAnimator.SetBool("moving", true);
                    }
                    else
                    {
                        enemyAnimator.SetBool("moving", false);
                    }
                }
            }
        }
    }
}
