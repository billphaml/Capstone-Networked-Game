using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent theGameEvent;

    // Start is called before the first frame update
    private void Awake()
    {
        theGameEvent = this;
    }


    public event Action <DialogueScene,int> onEndOfDialogueTrigger;

    public void onEndOfDialogue(DialogueScene theDialogueScene, int branchNum)
    {
        if(onEndOfDialogueTrigger != null)
        {
            onEndOfDialogueTrigger(theDialogueScene , branchNum);
        }
    }

}
