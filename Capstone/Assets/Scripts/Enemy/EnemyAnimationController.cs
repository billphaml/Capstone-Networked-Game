using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator enemyAnimator = null;
    [SerializeField]
    private GameObject enemy = null;
    private EnemyFSM enemyFSM = null;
    public EnemyFSM.EnemyState enemyState;

    private void Start()
    {
        if (enemy != null)
        {
            enemyFSM = enemy.GetComponent<EnemyFSM>();
        }
    }

    private void Update()
    {
        if (enemyFSM != null && enemyAnimator != null)
        {
            if (enemyFSM.getIsAttacking())
            {
                //Debug.Log(enemyFSM.getIsAttacking());
                enemyAnimator.SetBool("attacking", true);

                if (enemy.tag == "RangedEnemy")
                {
                    var newVec = (enemyFSM.transform.position - enemyFSM.getPlayerVector()).normalized;
                    //Debug.Log(newVec);

                    enemyAnimator.SetFloat("horizontal", newVec.x);
                    enemyAnimator.SetFloat("vertical", newVec.y);

                    enemyAnimator.SetBool("moving", true);
                }
            }
            else
            {
               // Debug.Log(enemyFSM.getIsAttacking());
                enemyAnimator.SetBool("attacking", false);

                var newVec = (enemyFSM.transform.position - enemyFSM.getPlayerVector()).normalized;
                //Debug.Log(newVec);

                enemyAnimator.SetFloat("horizontal", newVec.x);
                enemyAnimator.SetFloat("vertical", newVec.y);

                if (enemyFSM.GetEnemyState() == EnemyFSM.EnemyState.patrolState ||
                    enemyFSM.GetEnemyState() == EnemyFSM.EnemyState.chaseState)
                    enemyAnimator.SetBool("moving", true);
                else
                    enemyAnimator.SetBool("moving", false);
            }
        }
    }
}
