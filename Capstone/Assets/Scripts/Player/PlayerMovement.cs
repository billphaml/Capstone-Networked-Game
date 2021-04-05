using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using MLAPI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement.z = 0f;
        //Debug.Log(movement);
    }
        
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
            out var networkedClient))
        {
            var player = networkedClient.PlayerObject.GetComponent<NetworkPlayer>();
            if (player)
            {
                player.Move(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
