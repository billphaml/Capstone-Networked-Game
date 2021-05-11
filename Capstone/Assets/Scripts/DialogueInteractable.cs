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
        if (GameDialogueManager.theLocalGameManager.isDialogueActive == false)
        {
            npc.triggerDialogue();
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
