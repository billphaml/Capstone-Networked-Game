/******************************************************************************
 * Should trigger when players hit the object. Has a internal cooldown to
 * reset the hit after a while, done on the server.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using System.Collections;
using UnityEngine;
using MLAPI.Messaging;

public class PuzzleOnHit : PuzzleTrigger
{
    public bool isHit = false;

    [SerializeField] private float hitDuration = 15f;

    [ServerRpc]
    public void HitServerRpc()
    {
        if (!isHit)
        {
            isHit = true;
            TriggerServerRpc();
            Unhit();
        }
    }

    IEnumerator Unhit()
    {
        yield return new WaitForSeconds(hitDuration);
        isHit = false;
        UntriggerServerRpc();
    }
}
