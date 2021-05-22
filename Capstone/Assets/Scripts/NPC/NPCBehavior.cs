/******************************************************************************
 * Class to store the npc's stats. Also stores and triggers the npc's
 * dialogue.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class NPCBehavior : NetworkBehaviour
{
    public NPC actorIdentity;

    public DialogueScene theDialogue;

    public string npcName;

    public string npcDesc;

    public bool isIntro;

    // Start is called before the first frame update
    void Start()
    {
        StartSetup();
    }

    public void StartSetup()
    {
        actorIdentity = new NPC(npcName, npcDesc, Actor.actorType.NPC, Actor.attackType.SWORD);

        if (theDialogue != null)
        {
            actorIdentity.SetDialogue(theDialogue);
        }
        else
        {
            actorIdentity.SetDialogue((DialogueScene)Resources.Load("Scene_Dialogue/Golem_Slayer"));
        }

        GameEvent.theGameEvent.onEndOfDialogueTrigger += OnEndOfDialogue;
    }

    public void TriggerDialogue()
    {
        DialogueSystem.theLocalGameManager.StartDialogue(actorIdentity.GetDialogue());
    }

    private void OnEndOfDialogue(DialogueScene theDialogueScene, int branchNum)
    {
        if (actorIdentity.theDialogue == theDialogueScene)
        {
            DialogueScene returnDialogue = DialogueEventHandler.theDialogueHandler.ProcessDialogue(theDialogueScene, branchNum);
            if (returnDialogue != null) actorIdentity.theDialogue = returnDialogue;
        }
    }

    private void OnDestroy()
    {
        GameEvent.theGameEvent.onEndOfDialogueTrigger -= OnEndOfDialogue;
    }
}
