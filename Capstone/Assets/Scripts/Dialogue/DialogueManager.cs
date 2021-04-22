using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public GameObject textGroup;
    public TextMeshProUGUI dialogueDisplay;
    public TextMeshProUGUI placeholderDisplay;
    public TextMeshProUGUI activeDisplay;
    public TextMeshProUGUI nameDisplay;

    public Queue<Dialogue> queueDialogue; // Shows the actual dialogue
    public Queue<Dialogue> queueOneDialogue; // Prepare dialogue for 01 branch 
    public Queue<Dialogue> queueTwoDialogue; // Prepare dialogue for 02 branch
    public Queue<Dialogue> queueThreeDialogue; // Prepare dialogue for 03 branch

    private int index;
    private int currentIndex;
    private float textSpeed = 0.01f;
    private bool activeType;
    private bool canEnter;
    private float endTimer = 0.5f;
    private bool canNext;
    private bool isEndDialogue;
    private bool isActive;
    private Dialogue activeDialogue;




    // This IEnumerator is used to give the textDialogue for the NPC a typing effect.
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


    void Start()
    {
        queueDialogue = new Queue<Dialogue>();
        queueOneDialogue = new Queue<Dialogue>();
        queueTwoDialogue = new Queue<Dialogue>();
        queueThreeDialogue = new Queue<Dialogue>();
        
    }

    void Update()
    {
        dialogueController();
        updateEndDialogue();
    }


    // This method is used in order to help the player control the dialogue of the game.
    public void dialogueController()
    {
        if (!activeType)
        {
            if (canEnter == true && Input.GetKeyDown(KeyCode.Space))
            {
                nextDialogue();
            }
        }
        else
        {
            if (canEnter == true && Input.GetKeyDown(KeyCode.Space))
            {
                // Trigger The Event Tag 
                insertNextDialogue();
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


    public void startDialogue(string startScene)
    {
        // Find the dialogue set corresponding with the scene
        //string path = "Assets/Dialogue_Set/" + startScene + ".txt";
        GameDialogueManager.theLocalGameManager.turnOffPlayerMovement();
        turnOnDialogue();

        // Add a check for file
        TextAsset textFile = Resources.Load<TextAsset>("Scene_Dialogue/" + startScene);

        //Read all the lines in scene file
        //string[] theLine = File.ReadAllLines(path);

        string[] theLine = textFile.text.Split('\n');

        // Split the string line into three parts, Name, Dialogue and Type
        foreach (string importdialogue in theLine)
        {
            string[] splitDialogue = importdialogue.Split('\t');

            //Debug.Log(splitDialogue[0] + " " + splitDialogue[2] + " " + splitDialogue[4] + " " + splitDialogue[6] + " " + splitDialogue[8] + " " + splitDialogue[10]);

            Dialogue addNewDialogue = new Dialogue(splitDialogue[0], splitDialogue[2], splitDialogue[4], splitDialogue[6], splitDialogue[8], splitDialogue[10]);


            if (addNewDialogue.branchNum == 1 || addNewDialogue.branchNum == 4)
            {
                queueOneDialogue.Enqueue(addNewDialogue);
            } else if (addNewDialogue.branchNum == 2 || addNewDialogue.branchNum == 5)
            {
                queueTwoDialogue.Enqueue(addNewDialogue);
            } else if (addNewDialogue.branchNum == 3 || addNewDialogue.branchNum == 6)
            {
                queueThreeDialogue.Enqueue(addNewDialogue);
            }
            else
            {
                queueDialogue.Enqueue(addNewDialogue);
            }

        }
        nextDialogue();

        // Else
        // Dialogue errorDialogue = new Dialogue("Error", "This Scene Cannot Be Found", "false");
        // queueDialogue.Enqueue(errorDialogue);
        // nextDialogue();
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
    public void insertNextDialogue()
    {
        queueDialogue.Clear();

        switch (activeDialogue.branchNext)
        {
            case 1:
                while (queueOneDialogue.Count > 0)
                {
                    queueDialogue.Enqueue(queueOneDialogue.Dequeue());
                }
                break;
            case 2:
                while (queueTwoDialogue.Count > 0)
                {
                    queueDialogue.Enqueue(queueTwoDialogue.Dequeue());
                }
                break;
            case 3:
                while (queueThreeDialogue.Count > 0)
                {
                    queueDialogue.Enqueue(queueThreeDialogue.Dequeue());
                }
                break;
            case 4:
                while (queueOneDialogue.Count > 0)
                {
                    queueDialogue.Enqueue(queueOneDialogue.Dequeue());
                }
                break;
            case 5:
                while (queueTwoDialogue.Count > 0)
                {
                    queueDialogue.Enqueue(queueTwoDialogue.Dequeue());
                }
                break;
            case 6:
                while (queueThreeDialogue.Count > 0)
                {
                    queueDialogue.Enqueue(queueThreeDialogue.Dequeue());
                }
                break;
        }

        queueOneDialogue.Clear();
        queueTwoDialogue.Clear();
        queueThreeDialogue.Clear();
        nextDialogue();
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
                turnOffDialogue();
                isEndDialogue = false;
            }
        }
    }
}
