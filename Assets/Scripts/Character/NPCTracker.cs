using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTracker : MonoBehaviour
{

    NPCGenerator generator = null;

    public void OnDisable()
    {
        if(generator != null)
        {
            generator.RemoveNPC(this);
        }
    }

}
