/******************************************************************************
 * Can be called to drop an item from a drop table into the world.
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class EnemyItemDrop : NetworkBehaviour
{
    [SerializeField] private GameObject[] dropTable = null;

    public void SpawnDrop()
    {
        int randIndex = Random.Range(0, dropTable.Length);
        GameObject item = Instantiate(dropTable[randIndex], gameObject.transform.position, Quaternion.identity);
        item.GetComponent<NetworkObject>().Spawn();
    }
}
