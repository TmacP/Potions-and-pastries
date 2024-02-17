using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EInteractionType
{
    Primary,
    Secondary,
    Item
}

public interface IInteractable
{
    //Prompt to show on screen when we can interact
    public string InteractionPrompt { get; }
    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null);
}

public interface IInteractableExtension : IInteractable
{
    public string SecondaryInteractionPrompt { get; }

    public bool TrySecondaryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null);
}
