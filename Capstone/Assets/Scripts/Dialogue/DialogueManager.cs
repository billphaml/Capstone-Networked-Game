/******************************************************************************
*  This is the DialogueManager class.
 * The purpose of this class is to manage the dialoguescene scriptable object 
 * and pass it dialogue object through the queue to display to the player
 * Currently missing the ability to trigger events, apply buffs and change
 * values.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject textGroup;
    public TextMeshProUGUI dialogueDisplay;
    public TextMeshProUGUI placeholderDisplay;
    public TextMeshProUGUI activeDisplay;
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI timeDisplay;

    public Queue<Dialogue> queueDialogue; // Shows the actual dialogue

    private int index;
    private int currentIndex;
    private float textSpeed = 0.01f;
    private bool activeType;
    private bool canEnter;
    private float endTimer = 0.5f;
    private bool canNext;
    private bool isEndDialogue;
    private bool isActive;
    [SerializeField] private float dialogueTime;
    [SerializeField] private bool dialogueTimeActive;
    [SerializeField] private Dialogue activeDialogue;
    private DialogueScene currentDialogueScene;

    void Awake()
    {
        timeDisplay.enabled = false;
    }

    void Start()
    {
        queueDialogue = new Queue<Dialogue>();
    }

    void Update()
    {
        DialogueController();
        UpdateDialogueTimer();
        UpdateEndDialogue();
    }

    // This IEnumerator is used to give the textDialogue  a typing effect.
    IEnumerator DialogueTyping()
    {
        DequeueDisplayText();

        foreach (char letter in activeDialogue.dialogueText.ToCharArray())
        {
            dialogueDisplay.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        canEnter = true;
    }

    // This method is used in order to display the placeholder text for the players to type in
    public void DisplayText()
    {
        DequeueDisplayText();

        foreach (char letter in activeDialogue.dialogueText.ToCharArray())
        {
            placeholderDisplay.text += letter;
        }
    }

    public void DequeueDisplayText()
    {
        canEnter = false;
        activeDialogue = queueDialogue.Dequeue();
        activeType = activeDialogue.canType;
        nameDisplay.text = activeDialogue.speakerName;
    }

    // This method is used in order to help the player control the dialogue of the game.
    public void DialogueController()
    {
        if (!activeType)
        {
            if (canEnter == true && Input.GetKeyDown(KeyCode.Space))
            {
                ResponseHandler();
                NextDialogue();
            }
        }
        else
        {
            if (canEnter == true && Input.GetKeyDown(KeyCode.Space))
            {
                // Trigger The Event Tag 
                TurnOffTimer();
                InsertNextDialogue(activeDialogue.branchNext);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                for (int i = 0; i < queueDialogue.Count; i++)
                {
                    queueDialogue.Enqueue(activeDialogue);
                    NextDialogue();
                }

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                queueDialogue.Enqueue(activeDialogue);
                NextDialogue();
            }

            foreach (char letter in Input.inputString)
            {
                UserInput(letter);
            }
        }
    }

    public void UserInput(char uInput)
    {
        string activeWord = activeDialogue.dialogueText;
        Debug.Log(uInput);
        foreach (char letter in activeWord.ToCharArray())
        {
            if (canEnter == false)
            {
                if (GetLetter() == uInput)
                {
                    // Play typing sound
                    // Highlight letter and move onto the next letter
                    activeDisplay.text += uInput;
                    currentIndex++;

                    break;
                }
            }
        }

        if (EndDialogue())
        {
            canEnter = true;
        }
        else
        {
            canEnter = false;
        }
    }

    // This method is used to get the letter at the current index that the user is 
    public char GetLetter()
    {
        string activeWord = activeDialogue.dialogueText;

        char[] theChar = activeWord.ToCharArray();

        return theChar[currentIndex];
    }

    public void ResetTextBox()
    {
        currentIndex = 0;
        dialogueDisplay.text = "";
        placeholderDisplay.text = "";
        activeDisplay.text = "";
        nameDisplay.text = "";
    }

    public void StartDialogue(DialogueScene startScene)
    {
        // Find the dialogue set corresponding with the scene
        DialogueSystem.theLocalGameManager.TurnOffPlayerMovement();
        currentDialogueScene = startScene;
        TurnOnDialogue();

        for(int i = 0; i < currentDialogueScene.sceneDialogue.Length; i++)
        {
            if(currentDialogueScene.sceneDialogue[i].branchNum == 0)
            {
                queueDialogue.Enqueue(currentDialogueScene.sceneDialogue[i]);
            }
        }

        NextDialogue();
    }

    /// <summary>
    /// This method is used to move onto the next dialogue in the array.
    /// </summary>
    public void NextDialogue()
    {
        if (queueDialogue.Count > 0)
        {
            ResetTextBox();

            if (queueDialogue.Peek().canType == true)
            {
                
                DisplayText();
            }
            else
            {
                StartCoroutine(DialogueTyping());
            }

        }
        else
        {
            DialogueSystem.theLocalGameManager.TurnOnPlayerMovement();
            ResetTextBox();
            EndOfDialogue();
            // Animate and hide dialogue box
        }
    }


    /// <summary>
    /// This method is used to help insert dialogue following a player's choice
    /// </summary>
    /// <param name="iBranchNum"></param>
    public void InsertNextDialogue(int iBranchNum)
    {
        queueDialogue.Clear();
        
        for(int i = 0; i < currentDialogueScene.sceneDialogue.Length; i++)
        {
            if(currentDialogueScene.sceneDialogue[i].branchNum == iBranchNum)
            {
                queueDialogue.Enqueue(currentDialogueScene.sceneDialogue[i]);
            }
        }

        NextDialogue();
    }

    public void ResponseHandler()
    {
        if (activeDialogue.dialogueResponse.Length > 0)
        {
            QuestHandler();
            for (int i = 0; i < activeDialogue.dialogueResponse.Length; i++)
            {
                queueDialogue.Enqueue(activeDialogue.dialogueResponse[i]);
            }
            if(activeDialogue.typeTime >= 0)
            {
                TurnOnTimer();
            }
        }
    }

    /// <summary>
    /// This method is used to return whether the player is at the end of the dialogue string.
    /// </summary>
    /// <returns></returns>
    public bool EndDialogue()
    {
        bool endType = (currentIndex >= activeDialogue.dialogueText.Length);
        return endType;
    }

    // This method is used to turn on the dialogue, showing the textbox
    private void TurnOnDialogue()
    {
        textGroup.SetActive(true);
        isActive = true;
        DialogueSystem.theLocalGameManager.TurnOnDialogue();
    }

    // This method is used to turn off the dialogue, hiding the textbox
    private void TurnOffDialogue()
    {
        textGroup.SetActive(false);
        isActive = false;
        GiveQuest.theGiveQuest.closeQuest();
        DialogueSystem.theLocalGameManager.TurnOffDialogue();
    }

    private void TurnOnTimer()
    {
        dialogueTime = activeDialogue.typeTime;
        dialogueTimeActive = true;
        timeDisplay.enabled = true;
    }

    private void TurnOffTimer()
    {
        dialogueTime = activeDialogue.typeTime;
        dialogueTimeActive = false;
        timeDisplay.enabled = false;
    }

    public bool GetActive()
    {
        return isActive;
    }

    private void EndOfDialogue()
    {
        isEndDialogue = true;
        endTimer = 0.5f;
    }

    private void QuestHandler()
    {
        if (activeDialogue.theQuest != null)
        {
            GiveQuest.theGiveQuest.setQuest(activeDialogue.theQuest);
            GiveQuest.theGiveQuest.openQuest();
        }
    }
    
    private void UpdateEndDialogue()
    {
        if (isEndDialogue == true)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                GameEvent.theGameEvent.OnEndOfDialogue(currentDialogueScene, activeDialogue.branchNum);
                TurnOffDialogue();
                TurnOffTimer();
                isEndDialogue = false;
            }
        }
    }

    private void UpdateDialogueTimer()
    {
        if (dialogueTimeActive == true)
        {
            dialogueTime -= Time.deltaTime;
            int intTime = (int)dialogueTime + 1;
            timeDisplay.text = intTime.ToString() + "...";
            if (dialogueTime <= 0)
            {
                TurnOffTimer();
                InsertNextDialogue(-1);
            }
        }
    }
}
