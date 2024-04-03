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

public enum EInteractionResult
{
    Failure,
    Success,
    Success_ConsumeItem
}

public interface IInteractable
{
    //Prompt to show on screen when we can interact
    public string InteractionPrompt { get; }
    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null);
}

public interface IInteractableExtension : IInteractable
{
    public string GetSecondaryInteractionPrompt(InventoryItemData InteractionItem = null);

    public EInteractionResult TrySecondaryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null);

    public string GetThirdInteractionPrompt();
    public EInteractionResult TryThirdInteract(InteractorBehavoir InInteractor);


}
