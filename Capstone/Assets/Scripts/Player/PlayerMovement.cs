using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 movement;

    [SerializeField]
    private float moveSpeed = 7f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsLocalPlayer)
        {
            GrabInputPC();
            //GrabInputAndroid();
            //Debug.Log(movement);
        }
    }

    void GrabInputPC()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1f);
    }

    void GrabInputAndroid()
    {
    }

    private void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
