/******************************************************************************
 * Class to store the npc's stats. Also stores and triggers the npc's
 * dialogue.
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class NPCBehavior : NetworkBehaviour
{
    public NPC actorIdentity;
    public DialogueScene theDialogue;

    public string npcName;

    public string npcDesc;

    // Start is called before the first frame update
    void Start()
    {
        startSetup();
    }

    // Update is called once per frame
    void Update()
    {
        //theManager.getActive() == false
    }

    public void startSetup()
    {
        actorIdentity = new NPC(npcName, npcDesc, Actor.actorType.NPC, Actor.attackType.SWORD);
        if(theDialogue != null)
        {
            actorIdentity.setDialogue(theDialogue);
        }
        else
        {
            actorIdentity.setDialogue((DialogueScene)Resources.Load("Scene_Dialogue/Golem_Slayer"));
        }
        GameEvent.theGameEvent.onEndOfDialogueTrigger += onEndOfDialogue;
    }

    public void triggerDialogue()
    {
        DialogueSystem.theLocalGameManager.startDialogue(actorIdentity.getDialogue());
    }

    private void onEndOfDialogue(DialogueScene theDialogueScene, int branchNum)
    {
        if(actorIdentity.theDialogue == theDialogueScene)
        {
            DialogueScene returnDialogue = DialogueEventHandler.theDialogueHandler.ProcessDialogue(theDialogueScene, branchNum);
            if (returnDialogue != null) actorIdentity.theDialogue = returnDialogue;
        }
    }

    private void OnDestroy()
    {
        GameEvent.theGameEvent.onEndOfDialogueTrigger -= onEndOfDialogue;
    }
}
