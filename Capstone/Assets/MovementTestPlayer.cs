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
        float rand = Random.RandomRange(-10, 10);

        Vector3 newPos = new Vector3(rand, -rand + 8, 0);
        return newPos;
    }
}
