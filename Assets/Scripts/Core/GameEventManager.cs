using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EMiniGameCompleteResult
{
    CriticalSuccess,
    Success, 
    Failed, 
    Cancelled
}

public class GameEventManager : MonoBehaviour
{
    static public GameEventManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public event Action<EGameState, EGameState> OnChangeGameState;
    public void ChangeGameState(EGameState NewGameState, EGameState OldGameState)
    {
        if (OnChangeGameState != null)
        {
            OnChangeGameState(NewGameState, OldGameState);
        }
    }


    public event Action OnInteractionReleased;
    public void InteractionReleased()
    {
        if (OnInteractionReleased != null)
        {
            OnInteractionReleased();
        }
    }

    public event Action<IInteractable, InteractorBehavoir> OnChangeInteractionTarget;
    public void ChangeInteractionTarget(IInteractable NewTarget, InteractorBehavoir Interactor)
    {
        if(OnChangeInteractionTarget != null)
        {
            OnChangeInteractionTarget(NewTarget, Interactor);
        }
    }

    public event Action OnToggleInventory;
    public void ToggleInventory()
    {
        if (OnToggleInventory != null)
        {
            OnToggleInventory();
        }
    }

    public event Action<InventoryManager> OnInventoryOpen;
    public void InventoryOpen(InventoryManager inventoryManager)
    {
        if (OnInventoryOpen!= null)
        {
            OnInventoryOpen(inventoryManager);
        }
    }

    public event Action<InventoryManager> OnInventoryClosed;
    public void InventoryClosed(InventoryManager inventoryManager)
    {
        if (OnInventoryClosed != null)
        {
            OnInventoryClosed(inventoryManager);
        }
    }


    public event Action<List<ItemData>> OnGivePlayerItems;
    public void GivePlayerItems(List<ItemData> ItemData)
    {
        if(OnGivePlayerItems != null)
        {
            OnGivePlayerItems(ItemData);
        }
    }

    public event Action<List<ItemData>> OnRemovePlayerItems;
    public void RemovePlayerItems(List<ItemData> ItemData)
    {
        if(OnRemovePlayerItems != null)
        {
            OnRemovePlayerItems(ItemData);
        }
    }



    public event Action<EMiniGameCompleteResult> OnMiniGameComplete;
    public void MiniGameComplete(EMiniGameCompleteResult CompletionResult)
    {
        if(OnMiniGameComplete != null)
        {
            OnMiniGameComplete(CompletionResult);
        }
    }

}
