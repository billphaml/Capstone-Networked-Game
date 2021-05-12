/******************************************************************************
 * Data scriptable object containing information about a npc.
 *****************************************************************************/

[System.Serializable]
public class NPC : Actor
{
    public DialogueScene theDialogue;


    public NPC(string name, string description, actorType theType, attackType theAttack) : base(name, description, theType, theAttack)
    {

    }

    public NPC(Enemy iEnemy) : base(iEnemy.actorName, iEnemy.actorDescription, iEnemy.getActorType(), iEnemy.getAttackType())
    {

    }

    public NPC(NPC iNPC) : base(iNPC.actorName, iNPC.actorDescription, iNPC.getActorType(), iNPC.getAttackType())
    {

    }

    public void setDialogue(DialogueScene iDialogue)
    {
        theDialogue = iDialogue;
    }

    public DialogueScene getDialogue()
    {
        return theDialogue;
    }
}
