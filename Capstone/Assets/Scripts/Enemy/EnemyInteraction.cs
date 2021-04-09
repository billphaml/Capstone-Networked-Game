/******************************************************************************
 * Controls enemy interactions.
 *****************************************************************************/

// Uncomment for debug mode
//#define DEBUG
// Uncomment for normal mode
#undef DEBUG

using UnityEngine;
using MLAPI;

public class EnemyInteraction : NetworkBehaviour
{
    /// <summary>
    /// Reference to enemy controller to get other references i.e. player.
    /// </summary>
    private EnemyController ec = default;

    /// <summary>
    /// Enemy health.
    /// </summary>
    [SerializeField] private float health = 3f;

    //private ItemDrop itemDrop = default;

    #region Color 
    /// <summary>
    /// Original color.
    /// </summary>

    [SerializeField] private Color normalColor = Color.white;

    /// <summary>
    /// Damage color.
    /// </summary>
    [SerializeField] private Color damagedColor = new Color(222f / 255f, 139f / 255f, 137f / 255f, 197f / 255f);

    /// <summary>
    /// Time until switch from damaged to normal color.
    /// </summary>
    [SerializeField] private float damagedColorDuration = 60f;

    private bool countdown = false;
    #endregion

    void Start()
    {
        // Could probably set this to happen only on server, not sure if references waste memory
        ec = gameObject.GetComponent<EnemyController>();

        // Make sure to implement this as a server/host side thing.
        //itemDrop = GetComponent<ItemDrop>();
    }

    void Update()
    {
        if (IsHost || IsServer)
        {
            ColorCountdown();
        }
    }

    #region Enemy taking damage
    // deprecated function, needs to be reworked
    // functional that destroys enemy when health is equal to or less than zero.
    // also adds points based on enemy value.
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

    // Function to take projectile damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // hit by player bolt, damaged by amount given by
        // player behavior component
        if (collision.gameObject.tag == "PlayerBolt")
        {
            //PlayerBehavior i = player.GetComponent<PlayerBehavior>();
            ColorChange(damagedColor);
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

#if (DEBUG)
            Debug.Log(health);
#endif
        }
    }

    // deprecated, needs to be reworked into modular damage system
    // Function to take sniper damage
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
        if (countdown) damagedColorDuration--;

        if (damagedColorDuration < 0)
        {
            damagedColorDuration = 60;
            countdown = false;
            ColorChange(normalColor);
        }
    }
#endregion
}
