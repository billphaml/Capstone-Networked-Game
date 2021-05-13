/******************************************************************************
 * This Class is derived from interactable and is used to trigger dialogue
 * between the player and a npc.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 * ***************************************************************************/
using UnityEngine;

public class DialogueInteractable : Interactable
{
    [SerializeField] private NPCBehavior npc;

    private void Start()
    {
        npc = gameObject.GetComponent<NPCBehavior>();
    }

    protected override void Interact()
    {
        if (DialogueSystem.theLocalGameManager.isDialogueActive == false)
        {
            npc.TriggerDialogue();
        }
    }

    protected override void Update()
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
