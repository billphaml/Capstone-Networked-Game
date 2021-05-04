using System.Collections;
using System.Collections.Generic;
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
   


    public Dialogue( string characterName, string theDialogue, string isType)
    {
        speakerName = characterName;
        dialogueText = theDialogue;
        canType = convertType(isType);
    }

    public Dialogue(string bNum, string characterName, string theDialogue, string isType, string nextBranch, string tEvent)
    {
        branchNum = getBranchNum(bNum);
        speakerName = characterName;
        dialogueText = theDialogue;
        canType = convertType(isType);
        branchNext = getBranchNum(nextBranch);
    }

    public Dialogue(Dialogue iDialogue)
    {
        branchNum = iDialogue.branchNum;
        speakerName = iDialogue.speakerName;
        dialogueText = iDialogue.dialogueText;
        canType = iDialogue.canType;
        branchNext = iDialogue.branchNext;
    }

    private bool convertType(string isType)
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

    private int getBranchNum(string bNum)
    {
        int theBranchNum = 0;
        switch (bNum)
        {
            case "01":
                theBranchNum = 1;
                break;
            case "02":
                theBranchNum = 2;
                break;
            case "03":
                theBranchNum = 3;
                break;
            case "04":
                theBranchNum = 4;
                break;
            case "05":
                theBranchNum = 5;
                break;
            case "06":
                theBranchNum = 6;
                break;
        }

        return theBranchNum;
    }
}
