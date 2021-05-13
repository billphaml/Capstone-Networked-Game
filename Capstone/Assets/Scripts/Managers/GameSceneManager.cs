/******************************************************************************
 * So loading and syncing scenes is actually easy cause MLAPI does all of the
 * sync. So the only thing to make sure is that the scene that client has
 * active is the same as others on the server when they load in. So when a
 * client connects they need to make sure they have the same scene open.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.SceneManagement;

public class GameSceneManager : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        // Disconnect client

        //
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadLevelAdventure()
    {
        SceneManager.LoadScene("AdventureZone");
        
    }

    /// <summary>
    /// Maybe change this later to an event trigger to server.
    /// </summary>
    public void LoadDungeon()
    {
        if (IsServer)
        {
            NetworkSceneManager.SwitchScene("Boss");
        }
    }
}
