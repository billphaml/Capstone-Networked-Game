using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler theDialogueHandler;


    void Start()
    {
        theDialogueHandler = this;
    }

    public DialogueScene endDialogueHandler(DialogueScene theDialogue, int branchNum)
    {
        Debug.Log("The dialogueName is " + theDialogue.name);
        switch (theDialogue.name)
        {
            case "Scene_002":
                Debug.Log("Hi, how you're doing?");
                return introHandler(branchNum);
            break;

            default:
            return null;
        }

        
    }

    public DialogueScene introHandler(int branchNum)
    {
        switch (branchNum)
        {
            case 1:
                return (DialogueScene)Resources.Load("Scene_Dialogue/IntroGood");

            case 2:
                return (DialogueScene)Resources.Load("Scene_Dialogue/IntroNeutral");
            case 3:
                return (DialogueScene)Resources.Load("Scene_Dialogue/IntroBad");
            default:
                return (DialogueScene)Resources.Load("Scene_Dialogue/IntroBad");
        }
    }


}
