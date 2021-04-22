using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public NPC actorIdentity;

    // Start is called before the first frame update
    void Start()
    {
        actorIdentity = new NPC("Jason", "An Old Man In The Village", Actor.actorType.NPC, Actor.attackType.SWORD);
        actorIdentity.setDialogue("Scene_001");
    }

    // Update is called once per frame
    void Update()
    {
    //theManager.getActive() == false

        
    }

    public void triggerDialogue()
    {
        GameDialogueManager.theLocalGameManager.startDialogue(actorIdentity.getDialogue());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //canTalk = true; 
        Debug.Log("Wow you got here");
        if(GameDialogueManager.theLocalGameManager.dialogueActive == false)
        triggerDialogue();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //canTalk = false;
    }
}
