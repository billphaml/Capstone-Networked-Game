/******************************************************************************
 * Base class for open world puzzle triggers. Communicates with puzzle
 * controller to check if completion conditions are met. Triggers for puzzles
 * should derive from this class. Triggers are updated through serverrpcs so
 * that only the server knows when all triggers are true and so that the
 * server can spawn the reward.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PuzzleTrigger : NetworkBehaviour
{
    [SerializeField] private PuzzleController controller;

    public bool isTriggered = false;

    [ServerRpc(RequireOwnership = false)]
    protected void TriggerServerRpc()
    {
        isTriggered = true;
        controller.CheckTriggers();
    }

    [ServerRpc(RequireOwnership = false)]
    protected void UntriggerServerRpc()
    {
        isTriggered = false;
    }
}
