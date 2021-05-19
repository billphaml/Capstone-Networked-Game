/******************************************************************************
 * Should trigger when players enter the collider range.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class PuzzleOnTrigger : PuzzleTrigger
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            TriggerServerRpc();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            UntriggerServerRpc();
        }
    }
}
