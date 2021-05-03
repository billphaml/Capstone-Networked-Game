using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

/* This is the DialogueManager class.
 * The purpose of this class is to manage the dialoguescene scriptable object and pass it dialogue object through the queue to display to the player
 * Currently missing the ability to trigger events, apply buffs and change values.
 * 
 * 
 * 
 * 
 * */
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



    // This IEnumerator is used to give the textDialogue  a typing effect.
    IEnumerator dialogueTyping()
    {
        dequeueDisplayText();

        foreach (char letter in activeDialogue.dialogueText.ToCharArray())
        {
            dialogueDisplay.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        canEnter = true;
    }

    // This method is used in order to display the placeholder text for the players to type in
    public void displayText()
    {
        dequeueDisplayText();

        foreach (char letter in activeDialogue.dialogueText.ToCharArray())
        {
            placeholderDisplay.text += letter;
        }
    }

    public void dequeueDisplayText()
    {
        canEnter = false;
        activeDialogue = queueDialogue.Dequeue();
        activeType = activeDialogue.canType;
        nameDisplay.text = activeDialogue.speakerName;
    }

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
        dialogueController();
        updateDialogueTimer();
        updateEndDialogue();
    }


    // This method is used in order to help the player control the dialogue of the game.
    public void dialogueController()
    {
        if (!activeType)
        {
            if (canEnter == true && Input.GetKeyDown(KeyCode.Space))
            {
                responseHandler();
                nextDialogue();
            }
        }
        else
        {
            if (canEnter == true && Input.GetKeyDown(KeyCode.Space))
            {
                // Trigger The Event Tag 
                turnOffTimer();
                insertNextDialogue(activeDialogue.branchNext);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                for (int i = 0; i < queueDialogue.Count; i++)
                {
                    queueDialogue.Enqueue(activeDialogue);
                    nextDialogue();
                }

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                queueDialogue.Enqueue(activeDialogue);
                nextDialogue();
            }

            foreach (char letter in Input.inputString)
            {
                userInput(letter);
            }
        }
    }

    // This method is used to
    public void userInput(char uInput)
    {

        string activeWord = activeDialogue.dialogueText;
        Debug.Log(uInput);
        foreach (char letter in activeWord.ToCharArray())
        {
            if (canEnter == false)
            {
                if (getLetter() == uInput)
                {
                    // Play typing sound
                    // Highlight letter and move onto the next letter
                    activeDisplay.text += uInput;
                    currentIndex++;

                    break;
                }
            }
        }

        if (endDialogue())
        {
            canEnter = true;
        }
        else
        {
            canEnter = false;
        }
    }

    // This method is used to get the letter at the current index that the user is 
    public char getLetter()
    {
        string activeWord = activeDialogue.dialogueText;

        char[] theChar = activeWord.ToCharArray();

        return theChar[currentIndex];
    }

    public void resetTextBox()
    {
        currentIndex = 0;
        dialogueDisplay.text = "";
        placeholderDisplay.text = "";
        activeDisplay.text = "";
        nameDisplay.text = "";
    }


    public void startDialogue(DialogueScene startScene)
    {
        // Find the dialogue set corresponding with the scene
        GameDialogueManager.theLocalGameManager.turnOffPlayerMovement();
        currentDialogueScene = startScene;
        turnOnDialogue();

        for(int i = 0; i < currentDialogueScene.SceneDialogue.Length; i++)
        {
            if(currentDialogueScene.SceneDialogue[i].branchNum == 0)
            {
                queueDialogue.Enqueue(currentDialogueScene.SceneDialogue[i]);
            }
        }

        nextDialogue();
    }


    /* This method is used to move onto the next dialogue in the array.
     * 
     * 
     * 
     * */
    public void nextDialogue()
    {

        if (queueDialogue.Count > 0)
        {
            resetTextBox();

            if (queueDialogue.Peek().canType == true)
            {
                displayText();
            }
            else
            {
                StartCoroutine(dialogueTyping());
            }

        }
        else
        {
            resetTextBox();
            endOfDialogue();
            // Animate and hide dialogue box
        }
    }


    /* This method is used to help insert dialogue following a player's choice
     * 
     * 
     * 
     * */
    public void insertNextDialogue(int iBranchNum)
    {
        queueDialogue.Clear();
        
        for(int i = 0; i < currentDialogueScene.SceneDialogue.Length; i++)
        {
            if(currentDialogueScene.SceneDialogue[i].branchNum == iBranchNum)
            {
                queueDialogue.Enqueue(currentDialogueScene.SceneDialogue[i]);
            }
        }

        nextDialogue();
    }

    
    public void responseHandler()
    {
        if (activeDialogue.dialogueResponse.Length > 0)
        {
            for (int i = 0; i < activeDialogue.dialogueResponse.Length; i++)
            {
                queueDialogue.Enqueue(activeDialogue.dialogueResponse[i]);
            }
            if(activeDialogue.typeTime >= 0)
            {
                turnOnTimer();
            }
        }
    }

    // this method is used to return whether the player is at the end of the dialogue string
    public bool endDialogue()
    {
        bool endType = (currentIndex >= activeDialogue.dialogueText.Length);
        return endType;
    }

    // This method is used to turn on the dialogue, showing the textbox
    private void turnOnDialogue()
    {
        textGroup.SetActive(true);
        isActive = true;
        GameDialogueManager.theLocalGameManager.turnOnDialogue();
    }

    // This method is used to turn off the dialogue, hiding the textbox
    private void turnOffDialogue()
    {
        textGroup.SetActive(false);
        isActive = false;
        GameDialogueManager.theLocalGameManager.turnOffDialogue();
    }

    private void turnOnTimer()
    {
        dialogueTime = activeDialogue.typeTime;
        dialogueTimeActive = true;
        timeDisplay.enabled = true;
    }

    private void turnOffTimer()
    {
        dialogueTime = activeDialogue.typeTime;
        dialogueTimeActive = false;
        timeDisplay.enabled = false;
    }

    public bool getActive()
    {
        return isActive;
    }

    private void endOfDialogue()
    {
        isEndDialogue = true;
        endTimer = 0.5f;
    }
    
    private void updateEndDialogue()
    {
        if(isEndDialogue == true)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                GameEvent.theGameEvent.onEndOfDialogue(currentDialogueScene, activeDialogue.branchNum);
                turnOffDialogue();
                turnOffTimer();
                isEndDialogue = false;
            }
        }
    }

    private void updateDialogueTimer()
    {
        if(dialogueTimeActive == true)
        {
            dialogueTime -= Time.deltaTime;
            int intTime = (int)dialogueTime + 1;
            timeDisplay.text = intTime.ToString() + "...";
            if(dialogueTime <= 0)
            {
                turnOffTimer();
                insertNextDialogue(-1);
            }
        }
    }
}
