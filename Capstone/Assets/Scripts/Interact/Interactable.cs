/*******************************************************************************
 * This Class implements the interactable feature allowing objects with this
 * script attached to be interacted with. Specific interactions such as
 * opening dialogue or gathering should override the Interact() method with
 * the desired behavior.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * *****************************************************************************/

using UnityEngine;
using MLAPI;

public class Interactable : NetworkBehaviour
{
    [SerializeField] private GameObject interactUI = null;

    protected SpriteRenderer sr = null;

    protected bool isInteractDisplayed = false;

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

    protected virtual void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }

    private void Start()
    {
        // Get reference to interactable UI if needed (for spawned in interactables)

        interactUI = GameObject.FindGameObjectWithTag("Interactable UI");

        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (isInteractDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
    }
}
