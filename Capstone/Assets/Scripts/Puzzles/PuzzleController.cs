/******************************************************************************
 * Controller for the puzzle that checks if all triggers are activated and
 * if so will spawn a object (a treasure chest). Contains a cooldown until
 * the puzzle can be solved again.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;

public class PuzzleController : NetworkBehaviour
{
    [SerializeField] private PuzzleTrigger[] puzzleTriggers;

    private float nextPuzzleTime = 0f;

    [SerializeField] private float puzzleDelayTime = 60f;

    [SerializeField] private GameObject chestReward;

    /// <summary>
    /// Triggers call this method when they are set to true so that the
    /// controller knows to check all triggers for the spawn condition.
    /// </summary>
    public void CheckTriggers()
    {
        if (Time.time >= nextPuzzleTime)
        {
            foreach (PuzzleTrigger p in puzzleTriggers)
            {
                if (!p.isTriggered) return;
            }

            SpawnReward();

            nextPuzzleTime = Time.time + puzzleDelayTime;
        }
    }

    private void SpawnReward()
    {
        GameObject reward = Instantiate(chestReward, gameObject.transform.position, Quaternion.identity);
        reward.GetComponent<NetworkObject>().Spawn();
    }
}
