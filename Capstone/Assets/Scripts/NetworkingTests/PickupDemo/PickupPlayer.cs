using System.Collections;
using UnityEngine;
using MLAPI;

public class PickupPlayer : NetworkBehaviour
{
    [SerializeField] private PickupTarget target = null;

    private LineDrawer line;

    public int pickups = 0;

    public override void NetworkStart()
    {
        base.NetworkStart();

        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PickupTarget>();

        line = new LineDrawer();
    }

    public void Request()
    {
        Debug.Log("Executing...");
        line.DrawLineInGameView(gameObject.transform.position, target.transform.position, Color.blue);
        target.TryPickUpServerRpc(OwnerClientId);
        StartCoroutine(DelayDestroy());
    }

    public void RegisterPickup(PickupTarget target)
    {
        pickups++;
        target.MakeAvailableServerRpc();
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        line.Destroy();
    }
}
