using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class NPCBehavior : NetworkBehaviour
{
    public NPC actorIdentity;



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
        actorIdentity = new NPC("Jason", "An Old Man In The Village", Actor.actorType.NPC, Actor.attackType.SWORD);
        actorIdentity.setDialogue((DialogueScene)Resources.Load("Scene_Dialogue/Scene_002"));
        GameEvent.theGameEvent.onEndOfDialogueTrigger += onEndOfDialogue;
    }

    public void triggerDialogue()
    {
        GameDialogueManager.theLocalGameManager.startDialogue(actorIdentity.getDialogue());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>().IsLocalPlayer)
        {
            //canTalk = true; 
            Debug.Log("Wow you got here");
            if (GameDialogueManager.theLocalGameManager.dialogueActive == false)
                triggerDialogue();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //canTalk = false;
    }

    private void onEndOfDialogue(DialogueScene theDialogueScene, int branchNum)
    {
        
        if(actorIdentity.theDialogue == theDialogueScene)
        {
            if (DialogueHandler.theDialogueHandler.endDialogueHandler(theDialogueScene, branchNum) != null)
            actorIdentity.theDialogue = DialogueHandler.theDialogueHandler.endDialogueHandler(theDialogueScene, branchNum);
        }
    }

    private void OnDestroy()
    {
        GameEvent.theGameEvent.onEndOfDialogueTrigger -= onEndOfDialogue;
    }
}
