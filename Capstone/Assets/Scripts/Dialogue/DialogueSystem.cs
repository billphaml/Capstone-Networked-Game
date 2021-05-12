/******************************************************************************
 * Manages interactions between the dialogue system and the rest of the game.
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

    public void startDialogue(DialogueScene startScene)
    {
        theDialogueManager.StartDialogue(startScene);
    }

    public void turnOnDialogue()
    {
        isDialogueActive = true;
    }

    public void turnOffDialogue()
    {
        isDialogueActive = false;
    }

    public void turnOnPlayerMovement()
    {
        ThePlayerMovement.turnOnMove();
    }

    public void turnOffPlayerMovement()
    {
        ThePlayerMovement.turnOffMove();
    }
}
