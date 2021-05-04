/******************************************************************************
 * 
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer playerColor;

    [SerializeField] private  CMFindPlayer camera;

    [SerializeField] public PlayerStat stats;

    [SerializeField] private PlayerMovement move;

    [SerializeField] private PlayerAttack attack;

    // Start is called before the first frame update
    private void Start()
    {
        if (IsLocalPlayer)
        {
            playerColor.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            stats = gameObject.GetComponent<PlayerStat>();

            move = gameObject.GetComponent<PlayerMovement>();

            attack = gameObject.GetComponent<PlayerAttack>();

            camera = GameObject.FindGameObjectWithTag("CM Player").GetComponent<CMFindPlayer>();
            camera.Activate();
        }
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            move.UpdateMovement();

            attack.UpdateAttack();
        }
    }

    private void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            move.UpdateFixedMovement();
        }
    }
}
