/******************************************************************************
 * Takes in the finished dialogueScene and triggers an event.
 *****************************************************************************/

using UnityEngine;

public class DialogueEventHandler : MonoBehaviour
{
    public static DialogueEventHandler theDialogueHandler;

    void Awake()
    {
        theDialogueHandler = this;
    }

    public DialogueScene ProcessDialogue(DialogueScene theDialogue, int branchNum)
    {
        Debug.Log("The dialogueName is " + theDialogue.name);
        switch (theDialogue.name)
        {
            case "Scene_002":
                return TriggerIntro(branchNum);
            case "Golem_Slayer":
                return TriggerGolemSlayer(branchNum);
            case "Lost_Child_Intro":
                return TriggerLostChildIntro(branchNum);
            default:
                return null;
        }
    }

    public DialogueScene TriggerIntro(int branchNum)
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

    public DialogueScene TriggerLostChildIntro(int branchNum)
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

    public DialogueScene TriggerGolemSlayer(int branchNum)
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
