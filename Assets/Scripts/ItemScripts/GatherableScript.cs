using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableBehavoir : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "Gather";

    public bool TryInteract(InteractorBehavoir InInteractor)
    {
        Debug.Log("Gathering Items");
        return true;
    }
}
