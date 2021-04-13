using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 movement;
    public bool canMove;

    [SerializeField]
    private float moveSpeed = 7f;

    private Rigidbody2D rb;

    private FloatingJoystick stick = default;

    private void Awake()
    {
        canMove = true;
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (IsLocalPlayer)
        {
            try
            {
                stick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
            }
            catch
            {
            }
        }
    }

    void Update()
    {
        if (IsLocalPlayer && canMove)
        {
            GrabInputPC();
#if UNITY_ANDROID
            GrabInputAndroid();
#endif
        }
    }

    void GrabInputPC()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1f);
    }

    void GrabInputAndroid()
    {
        movement = new Vector3(stick.Horizontal, stick.Vertical, 0);
        movement = Vector3.ClampMagnitude(movement, 1f);
    }

    private void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
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
