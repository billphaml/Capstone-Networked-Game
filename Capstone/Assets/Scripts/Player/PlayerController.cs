using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer playerColor;

    // Start is called before the first frame update
    void Start()
    {
        playerColor.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
