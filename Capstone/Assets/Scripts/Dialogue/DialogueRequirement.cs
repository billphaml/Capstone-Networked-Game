using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRequirement : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<int, System.Action> requirementCheck = new Dictionary<int, System.Action>();

    void Start()
    {
        requirementCheck.Add(0, isDoneCheck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isDoneCheck()
    {
         //GameEvent.theGameEvent.isDone;
    }

}
