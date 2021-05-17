/******************************************************************************
 * This Class is the animation controller for the player
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class PlayerAnimation : NetworkBehaviour
{
    [SerializeField] private Animator animator = null;

    [SerializeField] private PlayerMovement move = null;

    private Vector2 moveDir;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        move = gameObject.GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (animator != null && IsLocalPlayer)
        {
            UpdateSpriteDir();

            UpdateMoveAnim();
        }
    }

    //Update player sprite facing direction
    private void UpdateSpriteDir()
    {
        Vector3 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDir.z = 0f;
        var newVec = (aimDir - transform.position).normalized;
        animator.SetFloat("horizontal", newVec.x);
        animator.SetFloat("vertical", newVec.y);
    }

    // Update player sprite moving/idle animation
    private void UpdateMoveAnim()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        if (move.canMove && (moveDir.x != 0 || moveDir.y != 0))
        {
            animator.SetFloat("speed", moveDir.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    }
}
