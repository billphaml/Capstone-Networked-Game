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

    [SerializeField] private float hitDuration;

    [SerializeField] private SpriteRenderer runes;

    [SerializeField] private float lerpSpeed;

    private Color curColor;

    private Color targetColor;

    [ServerRpc(RequireOwnership = false)]
    public void HitServerRpc()
    {
        if (!isHit)
        {
            isHit = true;
            EnableRunesClientRpc();
            TriggerServerRpc();
            StartCoroutine(Unhit());
        }
    }

    [ClientRpc]
    private void EnableRunesClientRpc()
    {
        targetColor = new Color(1, 1, 1, 1);
    }

    IEnumerator Unhit()
    {
        yield return new WaitForSeconds(hitDuration);
        isHit = false;
        DisableRunesClientRpc();
        UntriggerServerRpc();
    }

    [ClientRpc]
    private void DisableRunesClientRpc()
    {
        targetColor = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

        runes.color = curColor;
    }
}
