using UnityEngine;
using MLAPI;

public class Interactable : NetworkBehaviour
{
    [SerializeField] private GameObject interactUI = null;

    [SerializeField] private Sprite unharvested = null;

    [SerializeField] private Sprite harvested = null;

    [SerializeField] private Vector3 unharvestedSpriteOffset;

    [SerializeField] private Vector3 harvestedSpriteOffset;

    private SpriteRenderer sr = null;

    private bool isInteractDisplayed = false;

    private float nextHarvestTime = 0;

    private float harvestDelayTime = 180f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        if (!collision.gameObject.GetComponent<PlayerController>().IsLocalPlayer) return;

        isInteractDisplayed = true;
        interactUI.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Vector2 offset = new Vector2(0, gameObject.transform.lossyScale.y * 0.3f);
        interactUI.transform.position = new Vector2(gameObject.transform.position.x + offset.x, gameObject.transform.position.y + offset.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        if (!collision.gameObject.GetComponent<PlayerController>().IsLocalPlayer) return;

        isInteractDisplayed = false;
        interactUI.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }

    private void Start()
    {
        // Get reference to interactable UI if needed (for spawned in interactables)

        interactUI = GameObject.FindGameObjectWithTag("Interactable UI");

        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
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
