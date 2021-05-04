using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class NPCBehavior : NetworkBehaviour
{
    public NPC actorIdentity;
    public DialogueScene theDialogue;


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
        GameDialogueManager.theLocalGameManager.startDialogue(actorIdentity.getDialogue());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().IsLocalPlayer)
            {
                //canTalk = true; 
                Debug.Log("Wow you got here");
                if (GameDialogueManager.theLocalGameManager.isDialogueActive == false)
                    triggerDialogue();
            }
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
            DialogueScene returnDialogue = DialogueHandler.theDialogueHandler.endDialogueHandler(theDialogueScene, branchNum);
            if (returnDialogue != null)
            actorIdentity.theDialogue = returnDialogue;
        }
    }

    private void OnDestroy()
    {
        GameEvent.theGameEvent.onEndOfDialogueTrigger -= onEndOfDialogue;
    }
}
