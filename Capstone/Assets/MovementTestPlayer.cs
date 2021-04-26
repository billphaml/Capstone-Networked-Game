using UnityEngine;
using MLAPI;

public class MovementTestPlayer : NetworkBehaviour
{
    public override void NetworkStart()
    {
        base.NetworkStart();

    }

    public void Request()
    {
        Debug.Log("Executing...");        
        gameObject.transform.position = randomPos();
    }

    public Vector3 randomPos() 
    {
        Vector3 newPos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        return newPos;
    }
}
