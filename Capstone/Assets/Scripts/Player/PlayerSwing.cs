/******************************************************************************
 * This Class is the controller for the player's multi attack animation and
 * sound.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class PlayerSwing : MonoBehaviour
{
    private Animator animator = null;

    private PlayerAttack attack = null;

    private Vector2 moveDir;
    private int combo = 0;
    private bool test;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        attack = gameObject.GetComponentInParent<PlayerAttack>();
    }

    private void Update()
    {
        test = attack.GetisMeleeAttacking();
        if (animator != null && test == true)
        {

            if (combo == 0) Swing1();
            else if (combo == 1) Swing2();
            else if (combo == 2) Swing3();

        }
    }

    //plays the first swing animation
    private void Swing1()
    {
        animator.Play("Swing1");
        AudioManager._instance.Play("Attack1");
        combo = 1;
    }
    private void Swing2()
    {
        animator.Play("Swing2");
        AudioManager._instance.Play("Attack2");
        combo = 2;
    }
    private void Swing3()
    {
        animator.Play("Swing3");
        AudioManager._instance.Play("Attack3");
        combo = 0;
    }

}
