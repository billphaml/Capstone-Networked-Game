using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Actor
{
    private string dialoguePath;


    public NPC(string name, string description, actorType theType, attackType theAttack) : base(name, description, theType, theAttack)
    {

    }

    public NPC(Enemy iEnemy) : base(iEnemy.actorName, iEnemy.actorDescription, iEnemy.getActorType(), iEnemy.getAttackType())
    {

    }

    public NPC(NPC iNPC) : base(iNPC.actorName, iNPC.actorDescription, iNPC.getActorType(), iNPC.getAttackType())
    {

    }

    public void setDialogue(string iDialogue)
    {
        dialoguePath = iDialogue;
    }

    public string getDialogue()
    {
        return dialoguePath;
    }
}
