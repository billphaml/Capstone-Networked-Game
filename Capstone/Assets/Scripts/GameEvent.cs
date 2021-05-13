/******************************************************************************
 * Class to contain events that other scripts can trigger.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using System;
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

    public void OnEndOfDialogue(DialogueScene theDialogueScene, int branchNum)
    {
        if(onEndOfDialogueTrigger != null)
        {
            onEndOfDialogueTrigger(theDialogueScene , branchNum);
        }
    }
}
