/******************************************************************************
 * Manages interactions between the dialogue system and the rest of the game.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using MLAPI;

public class DialogueSystem : NetworkBehaviour
{
    private static DialogueSystem _theLocalgameManager;

    public static DialogueSystem theLocalGameManager { get { return _theLocalgameManager; } }

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

    public void StartDialogue(DialogueScene startScene)
    {
        theDialogueManager.StartDialogue(startScene);
    }

    public void TurnOnDialogue()
    {
        isDialogueActive = true;
    }

    public void TurnOffDialogue()
    {
        isDialogueActive = false;
    }

    public void TurnOnPlayerMovement()
    {
        ThePlayerMovement.turnOnMove();
    }

    public void TurnOffPlayerMovement()
    {
        ThePlayerMovement.turnOffMove();
    }
}
