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
        ec = gameObject.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemyFSM = enemy.GetComponent<EnemyFSM>();
        }
    }

    private void Update()
    {
        if (enemyFSM != null && enemyAnimator != null)
        {
            if (enemyFSM.GetIsAttacking())
            {
                //Debug.Log(enemyFSM.getIsAttacking());
                enemyAnimator.SetBool("attacking", true);

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
               ///Debug.Log(enemyFSM.getIsAttacking());
                enemyAnimator.SetBool("attacking", false);

                var newVec = (ec.transform.position - ec.player.transform.position).normalized;
                //Debug.Log(newVec);
                
                enemyAnimator.SetFloat("horizontal", newVec.x);
                enemyAnimator.SetFloat("vertical", newVec.y);
                
                if (newVec.x < 0)
                {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
                }
                if (newVec.x > 0)
                {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                }

                if (enemyFSM.GetEnemyState() == EnemyFSM.EnemyState.patrolState ||
                    enemyFSM.GetEnemyState() == EnemyFSM.EnemyState.chaseState)
                    enemyAnimator.SetBool("moving", true);
                else
                    enemyAnimator.SetBool("moving", false);
            }
        }
    }
}
