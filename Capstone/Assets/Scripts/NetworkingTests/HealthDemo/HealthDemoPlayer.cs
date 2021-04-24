using UnityEngine;
using MLAPI;

public class HealthDemoPlayer : NetworkBehaviour
{
    [SerializeField] private HealthDemoTarget target = null;

    public override void NetworkStart()
    {
        base.NetworkStart();

        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthDemoTarget>();
    }

    public void Request()
    {
        Debug.Log("Executing...");
        bool rand = System.Convert.ToBoolean(Random.Range(0, 2));

        if (rand)
        {
            target.AddHealthServerRpc(10);
        }
        else
        {
            target.RemoveHealthServerRpc(10);
        }
    }
}
