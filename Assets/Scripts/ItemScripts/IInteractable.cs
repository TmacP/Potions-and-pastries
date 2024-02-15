using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    
    //Prompt to show on screen when we can interact
    public string InteractionPrompt { get; }
    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null);
}
