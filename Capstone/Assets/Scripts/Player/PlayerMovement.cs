using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    public float moveSpeed = 7f;

    public Rigidbody2D controller;
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }
        
    // Update is called once per frame
    private void FixedUpdate()
    {
        controller.MovePosition(controller.position + movement *moveSpeed * Time.fixedDeltaTime);
    }
}
