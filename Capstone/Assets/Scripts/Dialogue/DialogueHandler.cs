using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler theDialogueHandler;

    void Awake()
    {
        theDialogueHandler = this;
    }

    public DialogueScene endDialogueHandler(DialogueScene theDialogue, int branchNum)
    {
        Debug.Log("The dialogueName is " + theDialogue.name);
        switch (theDialogue.name)
        {
            case "Scene_002":
                return introHandler(branchNum);
            case "Golem_Slayer":
                return GolemSlayerHandler(branchNum);
            case "Lost_Child_Intro":
                return LostChildIntroHandler(branchNum);
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

    public DialogueScene LostChildIntroHandler(int branchNum)
    {
        switch (branchNum)
        {
            case 1:
                GiveQuest.theGiveQuest.acceptQuest();
                return (DialogueScene)Resources.Load("Scene_Dialogue/Lost_Child_Accept");
            default:
                return null;
        }
    }

    public DialogueScene GolemSlayerHandler(int branchNum)
    {
        switch (branchNum)
        {
            case 1:
                GiveQuest.theGiveQuest.acceptQuest();
                return (DialogueScene)Resources.Load("Scene_Dialogue/AfterGenericQuest");
            default:
                return null;
        }
    }


}
