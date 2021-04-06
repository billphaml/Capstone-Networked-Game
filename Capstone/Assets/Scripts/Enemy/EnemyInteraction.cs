using UnityEngine;
using UnityEngine.AI;

public class EnemyInteraction : MonoBehaviour
{
    //private ItemDrop itemDrop = default;
    private GameObject player;

    public EnemyFSM state;
    public EnemyFSM.EnemyState enemyState;

    [SerializeField]
    private float health = 3f;

    #region Color 
    // Original color
    private Color normalColor = Color.white;
    // Damage color
    private Color newColor = new Color(222f / 255f, 139f / 255f, 137f / 255f, 197f / 255f);
    // color timer
    private float timer = 60;
    private bool countdown = false;
    #endregion

    // Enemy NavMesh controller
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //itemDrop = GetComponent<ItemDrop>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        enemyState = EnemyFSM.EnemyState.patrolState;
        state = gameObject.GetComponent<EnemyFSM>();

        // Get agent component in enemy
        agent = GetComponent<NavMeshAgent>();

        state.setState(EnemyFSM.EnemyState.patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        ColorCountdown();

        ProcessHealth();

        state.meleeMovementFSM();

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < state.getChaseRange())
        {
            state.setState(EnemyFSM.EnemyState.chaseState);

            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= state.getAttackRange())
            {
                state.setState(EnemyFSM.EnemyState.attackState);
            }
        }
    }

    #region Enemy taking damage
    // function that destroys itself once health is lower than 1
    private void ProcessHealth()
    {
        if (health <= 0)
        {
            //itemDrop.DropItem();

            if (gameObject.tag.Equals("Adware"))
            {
                //ScoreController.AddDestroyCount(25);
            }

            if (gameObject.tag.Equals("Brute"))
            {
                //ScoreController.AddDestroyCount(20);
            }

            if (gameObject.tag.Equals("MeleeEnemy"))
            {
                //ScoreController.AddDestroyCount(10);
            }

            if (gameObject.tag.Equals("RangedEnemy"))
            {
                //ScoreController.AddDestroyCount(15);
            }

            if (gameObject.tag.Equals("Trojan"))
            {
                //ScoreController.AddDestroyCount(20);
            }

            if (gameObject.tag.Equals("Brute Chieftain"))
            {
                //GameController.VictoryScreen();
            }

            //FloorGameController.numberOfEnemies--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // hit by player bolt, damaged by amount given by
        // player behavior component
        if (collision.gameObject.tag == "PlayerBolt")
        {
            //PlayerBehavior i = player.GetComponent<PlayerBehavior>();
            ColorChange(newColor);
            countdown = true;
            if (collision != null)
            {
                //ShotBehavior temp = collision.gameObject.GetComponent<ShotBehavior>();
                //if (temp.IsCrit())
                //{
                //    health -= i.EnemyDamaged() * 1.5f;
                //}
                //else
                //{
                //    health -= i.EnemyDamaged();
                //}
            }
           
            Debug.Log(health);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "PlayerSniperBolt") // damge twice
        //{
        //    PlayerBehavior i = player.GetComponent<PlayerBehavior>();
        //    ColorChange(newColor);
        //    countdown = true;
        //    if (collision != null) // add crit damage to bullet
        //    {
        //        SniperShotBehavior temp = collision.gameObject.GetComponent<SniperShotBehavior>();
        //        if (temp.IsCrit())
        //        {
        //            health -= i.EnemyDamaged() * 3 * 1.5f;
        //        }
        //        else
        //        {
        //            health -= i.EnemyDamaged() * 3;
        //        }
        //    }
        //    Debug.Log(health);
        //}
    }

    public void TrojanDamage(float trojanDamageAmount)
    {
        health -= trojanDamageAmount;
    }

    // Color changing function for when damaged
    private void ColorChange(Color c)
    {
        GameObject enemySprite = transform.Find("Enemy Sprite").gameObject;
        enemySprite.GetComponent<SpriteRenderer>().color = c;
    }
    // Color change timer support
    private void ColorCountdown()
    {
        if (countdown)
            timer--;
        if (timer < 0)
        {
            timer = 60;
            countdown = false;
            ColorChange(normalColor);
        }
    }
    #endregion

}
