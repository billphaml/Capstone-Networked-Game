/******************************************************************************
 * This Class is derived from interactable, and is used to create a chest
 * object that can be opened to give an item. The chest is removed after being
 * interacted with.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ChestInteractable : Interactable
{
    [SerializeField] private ItemBehavior[] items;

    protected override void Interact()
    {
        Debug.Log("Interacted with " + transform.name);

        var num = Random.Range(0, items.Length - 1);
        var item = items[num];
        NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.GetComponent<PlayerStat>().AddItemPrefab(item);
    }

    public override void Start()
    {
        base.Start();

        AudioManager._instance.Play("ChestSpawn");
    }

    protected override void Update()
    {
        if (isInteractDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
                DestroyServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyServerRpc()
    {
        Destroy(gameObject);
    }
}
