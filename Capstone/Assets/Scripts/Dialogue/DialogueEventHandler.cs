/******************************************************************************
 * Takes in the finished dialogueScene and triggers an event.
 * 
 * Authors: Bill, Hamza, Max, Ryan
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
            case "Cain_Intro":
                return TriggerCainIntro();
            case "Eser_Intro":
                return TriggerEserIntro();
            case "Barone_Intro":
                return TriggerBaroneIntro();
            case "George_Intro":
                return TriggerGeorgeIntro();
            case "Lale_Intro":
                return TriggerLaleIntro();
            case "Oswin_Intro":
                return TriggerOswinIntro();
            case "Roland_Intro":
                return TriggerRolandIntro();
            case "George_Default":
                return TriggerGeorgeDefault();
            case "Lale_Default":
                return TriggerLaleDefault();
            case "Oswin_Default":
                return TriggerOswinDefault();
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

    public DialogueScene TriggerEserIntro()
    {
      return (DialogueScene)Resources.Load("Scene_Dialogue/Eser_Default");
    }

    public DialogueScene TriggerCainIntro()
    {
        return (DialogueScene)Resources.Load("Scene_Dialogue/Cain_Default");
    }

    public DialogueScene TriggerBaroneIntro()
    {
        return (DialogueScene)Resources.Load("Scene_Dialogue/Golem_Slayer");
    }

    public DialogueScene TriggerGeorgeIntro()
    {
        return (DialogueScene)Resources.Load("Scene_Dialogue/George_Default");
    }

    public DialogueScene TriggerLaleIntro()
    {
        return (DialogueScene)Resources.Load("Scene_Dialogue/Lale_Default");
    }

    public DialogueScene TriggerOswinIntro()
    {
        return (DialogueScene)Resources.Load("Scene_Dialogue/Oswin_Default");
    }

    public DialogueScene TriggerRolandIntro()
    {
        return (DialogueScene)Resources.Load("Scene_Dialogue/Roland_Default");
    }

    public DialogueScene TriggerGeorgeDefault()
    {
        ShopManager.theShopManager.triggerShop("George's Smithing Workshop");
        return null;
    }

    public DialogueScene TriggerLaleDefault()
    {
        ShopManager.theShopManager.triggerShop("Lale Silversmith Workshop");
        return null;
    }

    public DialogueScene TriggerOswinDefault()
    {
        ShopManager.theShopManager.triggerShop("Oswin Apothecary");
        return null;
    }

}
