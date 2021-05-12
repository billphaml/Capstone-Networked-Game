/******************************************************************************
 * This Class is used to facilitate the local movement for the player.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LocalPlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    public bool canMove;

    [SerializeField]
    private float moveSpeed = 7f;

    private Rigidbody2D rb;


    private void Awake()
    {
        canMove = true;
        
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    
    }


    void Update()
    {
        GrabInputPC();

        if (Input.GetKeyDown(KeyCode.P))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }
    }

    void GrabInputPC()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1f);
    }

    private void FixedUpdate()
    {
         rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void turnOnMove()
    {
        canMove = true;
    }

    public void turnOffMove()
    {
        canMove = false;
    }
}
