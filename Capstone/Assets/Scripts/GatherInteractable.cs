/******************************************************************************
 * This Class is derived from interactable, and is used to create an item that 
 * can be harvested. Must override the interactable class.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using UnityEngine;
using MLAPI;

public class GatherInteractable : Interactable
{
    [SerializeField] private ItemBehavior item = null;

    [SerializeField] private Sprite unharvested = null;

    [SerializeField] private Sprite harvested = null;

    [SerializeField] private Vector3 unharvestedSpriteOffset;

    [SerializeField] private Vector3 harvestedSpriteOffset;

    private float nextHarvestTime = 0;

    [SerializeField] private float harvestDelayTime = 180f;

    protected override void Interact()
    {
        Debug.Log("Interacted with " + transform.name);

        NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.GetComponent<PlayerStat>().AddItemPrefab(item);
    }

    protected override void Update()
    {
        if (isInteractDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.F) && nextHarvestTime <= Time.time)
            {
                nextHarvestTime = Time.time + harvestDelayTime;
                sr.sprite = harvested;
                sr.gameObject.transform.position = gameObject.transform.position + harvestedSpriteOffset;
                Interact();
            }
        }

        if (nextHarvestTime <= Time.time)
        {
            sr.sprite = unharvested;
            sr.gameObject.transform.position = gameObject.transform.position + unharvestedSpriteOffset;
        }
    }
}
