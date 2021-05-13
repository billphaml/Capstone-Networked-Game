/******************************************************************************
 * Data scriptable object containing information about a npc.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

[System.Serializable]
public class NPC : Actor
{
    public DialogueScene theDialogue;

    public NPC(string name, string description, actorType theType, attackType theAttack) : base(name, description, theType, theAttack)
    {

    }

    public NPC(Enemy iEnemy) : base(iEnemy.actorName, iEnemy.actorDescription, iEnemy.GetActorType(), iEnemy.GetAttackType())
    {

    }

    public NPC(NPC iNPC) : base(iNPC.actorName, iNPC.actorDescription, iNPC.GetActorType(), iNPC.GetAttackType())
    {

    }

    public void SetDialogue(DialogueScene iDialogue)
    {
        theDialogue = iDialogue;
    }

    public DialogueScene GetDialogue()
    {
        return theDialogue;
    }
}
