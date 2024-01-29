using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStationScript : MonoBehaviour, IInteractable
{

    public CraftingStationData Data;

//************ IINteractable
    public string InteractionPrompt => Data.InteractionPrompt;

    public bool TryInteract(InteractorBehavoir InInteractor)
    {
        //Open Crafting UI screen

        return false;
    }

//********* End of IInteractable

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
