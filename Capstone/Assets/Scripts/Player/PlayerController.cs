using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer playerColor;

    [SerializeField] private new GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            camera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        }

        playerColor.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
