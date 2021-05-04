/******************************************************************************
 * Script to rotate hitbox around the player based on mouse direction.
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class AttackBoxLocation : NetworkBehaviour
{
    [SerializeField] private Transform player = null;

    [SerializeField] private float radius = 1;

    private Transform pivot;

    void Start()
    {
        if (IsLocalPlayer)
        {
            pivot = player.transform;
            transform.parent = pivot;
            transform.position += Vector3.up * radius;
        }
    }

    void Update()
    {
        if (IsLocalPlayer)
        {
            Vector3 playerVector = Camera.main.WorldToScreenPoint(player.position);
            playerVector = Input.mousePosition - playerVector;
            float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;

            pivot.position = player.position;
            pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
