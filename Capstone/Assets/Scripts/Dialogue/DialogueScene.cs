/******************************************************************************
 * Scriptable object to store dialogue blocks which make up a scene of
 * dialogue.
 *****************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Scene", menuName = "Dialogue/Scene")]
public class DialogueScene : ScriptableObject
{
    public Dialogue[] SceneDialogue;
}
