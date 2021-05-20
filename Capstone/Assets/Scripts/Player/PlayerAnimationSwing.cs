/******************************************************************************
 * This Class is the animation controller for the player's sqing animation
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class PlayerAnimationSwing : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    [SerializeField] private PlayerAttack attack = null;

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
            else Swing3();

        }
    }

    //plays the first swing animation
    private void Swing1()
    {
        animator.Play("Swing1");
        combo++;
    }
    private void Swing2()
    {
        animator.Play("Swing2");
        combo++;
    }
    private void Swing3()
    {
        animator.Play("Swing1");
        combo = 0;
    }

}
