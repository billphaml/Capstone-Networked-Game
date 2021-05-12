/******************************************************************************
 * Class to create instances of dialogue for DialogueScene.cs. Each instance
 * is a block of dialogue that belongs to a dialogue scene.
 *****************************************************************************/

using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string eventPrerequisite;

    public int branchNum;

    public string speakerName;

    [TextArea(5,10)]
    public string dialogueText;

    public bool canType;

    public float typeTime;

    public int branchNext;

    public Quest theQuest;

    public Dialogue[] dialogueResponse;
   
    public Dialogue(string characterName, string theDialogue, string isType)
    {
        speakerName = characterName;
        dialogueText = theDialogue;
        canType = ConvertType(isType);
    }

    public Dialogue(string bNum, string characterName, string theDialogue, string isType, string nextBranch, string tEvent)
    {
        speakerName = characterName;
        dialogueText = theDialogue;
        canType = ConvertType(isType);
    }

    public Dialogue(Dialogue iDialogue)
    {
        branchNum = iDialogue.branchNum;
        speakerName = iDialogue.speakerName;
        dialogueText = iDialogue.dialogueText;
        canType = iDialogue.canType;
        branchNext = iDialogue.branchNext;
    }

    private bool ConvertType(string isType)
    {
        if (isType.Equals("true") || isType.Equals("True"))
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
