using UnityEngine;
using MLAPI;

public class GatherInteractable : Interactable
{
    [SerializeField] private ItemBehavior item = null;

    public override void Interact()
    {
        Debug.Log("Interacted with " + transform.name);

        NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.GetComponent<PlayerStat>().AddItemPrefab(item);
    }
}
