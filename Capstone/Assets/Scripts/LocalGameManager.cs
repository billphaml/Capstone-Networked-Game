using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameManager : MonoBehaviour
{
    private static LocalGameManager _theLocalgameManager;

    public static LocalGameManager theLocalGameManager { get { return _theLocalgameManager; } }

    public DialogueManager theDialogueManager;
    public PlayerMovement ThePlayerMovement;

    public bool dialogueActive;
    

     void Awake()
    {
     if( _theLocalgameManager != null && _theLocalgameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _theLocalgameManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void startDialogue(string startScene)
    {
        theDialogueManager.startDialogue(startScene);
    }


    public void turnOnDialogue()
    {
        dialogueActive = true;
    }

    public void turnOffDialogue()
    {
        dialogueActive = false;
    }

    //
    public void turnOnPlayerMovement()
    {
        ThePlayerMovement.turnOnMove();
    }

    //
    public void turnOffPlayerMovement()
    {
        ThePlayerMovement.turnOffMove();
    }


}
