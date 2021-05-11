using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class GameDialogueManager : NetworkBehaviour
{
    private static GameDialogueManager _theLocalgameManager;

    public static GameDialogueManager theLocalGameManager { get { return _theLocalgameManager; } }

    public DialogueManager theDialogueManager;
    private PlayerMovement ThePlayerMovement;

    public bool isDialogueActive;
    
    void Awake()
    {
        if (_theLocalgameManager != null && _theLocalgameManager != this)
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
        if (ThePlayerMovement == null)
        {
            try
            {
                ThePlayerMovement = NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.gameObject.GetComponent<PlayerMovement>();
            }
            catch
            {
            }
        }
    }

    //
    public void startDialogue(DialogueScene startScene)
    {
        theDialogueManager.startDialogue(startScene);
    }


    public void turnOnDialogue()
    {
        isDialogueActive = true;
    }

    public void turnOffDialogue()
    {
        isDialogueActive = false;
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
