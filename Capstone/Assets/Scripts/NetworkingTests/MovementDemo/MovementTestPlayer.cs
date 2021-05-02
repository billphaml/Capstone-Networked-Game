using UnityEngine;

public class MovementTestPlayer : MonoBehaviour
{
    public void Request()
    {
        Debug.Log("Executing...");        
        gameObject.transform.position = randomPos();
    }

    public Vector3 randomPos()
    {
        return new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
    }
}
