using UnityEngine;
using MLAPI;
using System.Collections;

public class HealthDemoPlayer : NetworkBehaviour
{
    [SerializeField] private HealthDemoTarget target = null;

    private LineDrawer line;

    public override void NetworkStart()
    {
        base.NetworkStart();

        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthDemoTarget>();

        line = new LineDrawer();
    }

    public void Request()
    {
        Debug.Log("Executing...");
        bool rand = System.Convert.ToBoolean(Random.Range(0, 2));
        line.DrawLineInGameView(gameObject.transform.position, target.transform.position, Color.blue);

        if (rand)
        {
            target.AddHealthServerRpc(10);
        }
        else
        {
            target.RemoveHealthServerRpc(10);
        }

        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1);
        line.Destroy();
    }
}
