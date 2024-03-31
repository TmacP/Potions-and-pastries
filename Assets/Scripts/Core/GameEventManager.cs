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

    public event Action<InventoryManager> OnInventoryOpen;
    public void InventoryOpen(InventoryManager inventoryManager)
    {
        if (OnInventoryOpen!= null)
        {
            OnInventoryOpen(inventoryManager);
        }
    }






    public event Action OnClosePauseMenu;
    public void ClosePauseMenu()
    {
        if (OnClosePauseMenu != null)
        {
            OnClosePauseMenu();
        }
    }








    public event Action OnCloseMenu;
    public void CloseMenu()
    {
        if (OnCloseMenu != null)
        {
            OnCloseMenu();
        }
    }

    public event Action OnPostInventoryOpen;
    public void PostInventoryOpen()
    {
        if (OnPostInventoryOpen != null)
        {
            OnPostInventoryOpen();
        }
    }

    public event Action<int> OnPurchase;
    public void Purchase(int Cost)
    {
        if (OnPurchase != null)
        {
            OnPurchase(Cost);
        }
    }

    public event Action<long, long > OnPostPlayerGoldChanged;
    public void PostPlayerGoldChanged(long NewGoldValue, long DeltaGold)
    {
        if (OnPostPlayerGoldChanged != null)
        {
            OnPostPlayerGoldChanged(NewGoldValue, DeltaGold);
        }
    }

    public event Action<List<InventoryItemData>> OnGivePlayerItems;
    public void GivePlayerItems(List<InventoryItemData> ItemData)
    {
        if(OnGivePlayerItems != null)
        {
            OnGivePlayerItems(ItemData);
        }
    }

    public event Action<int> OnCraftComplete;
    public void CraftComplete(int CardsToDraw)
    {
        if (OnCraftComplete != null)
        {
            OnCraftComplete(CardsToDraw);
        }
    }

    public event Action<List<InventoryItemData>> OnRemovePlayerItems;
    public void RemovePlayerItems(List<InventoryItemData> ItemData)
    {
        if(OnRemovePlayerItems != null)
        {
            OnRemovePlayerItems(ItemData);
        }
    }

    public event Action OnRefreshInventory;
    public void RefreshInventory()
    {
        if (OnRefreshInventory != null)
        {
            OnRefreshInventory();
        }
    }

    public event Action<RecipeData> OnRecipeDataSelected;
    public void RecipeDataSelected(RecipeData InRecipeData)
    {
        if(OnRecipeDataSelected != null)
        {
            OnRecipeDataSelected(InRecipeData);
        }
    }

    public event Action<QuestData> OnQuestGiven;
    public void QuestGiven(QuestData newQuest)
    {
        if (OnQuestGiven != null)
        {
            OnQuestGiven(newQuest);
        }
    }

    public event Action OnQuestStatusRefreshed;
    public void QuestStatusRefreshed()
    {
        if (OnQuestStatusRefreshed != null)
        {
            OnQuestStatusRefreshed();
        }
    }

    public event Action<QuestData> OnQuestComplete;
    public void QuestComplete(QuestData Quest)
    {
        if (OnQuestComplete != null)
        {
            OnQuestComplete(Quest);
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

    public event Action<EGameRegion> OnUnlockRegion;
    public void UnlockRegion(EGameRegion Region)
    {
        if (OnUnlockRegion != null)
        {
            OnUnlockRegion(Region);
        }
    }

    public event Action<int> OnDoorUnlocked;
    public void DoorUnlocked(int DoorID)
    {
        if (OnDoorUnlocked != null)
        {
            OnDoorUnlocked(DoorID);
        }
    }



    public event Action<OrderData> OnTakeNPCOrder;
    public void TakeNPCOrder(OrderData NPCOrderData)
    {
        if (OnTakeNPCOrder != null)
        {
            OnTakeNPCOrder(NPCOrderData);
        }
    }


    //This is redundant use OnNPCRecieveOrder instead
    public event Action<OrderData> OnDoneNPCOrder;
    public void DoneNPCOrder(OrderData NPCOrderData)
    {
        if (OnDoneNPCOrder != null)
        {
            OnDoneNPCOrder(NPCOrderData);
        }
    }

    public event Action OnNPCRecieveOrder;
    public void NPCRecieveOrder()
    {
        if (OnNPCRecieveOrder != null)
        {
            OnNPCRecieveOrder();
        }
    }

    public event Action<GameObject> OnNPCLeavingChair;
    public void NPCLeavingChair(GameObject NPC)
    {
        if (OnNPCLeavingChair != null)
        {
            OnNPCLeavingChair(NPC);
        }
    }


    public event Action<DialogueData> OnShowDialogueQuip;
    public void ShowDialogueQuip(DialogueData DialogueQuip)
    {
        if(OnShowDialogueQuip != null)
        {
            OnShowDialogueQuip(DialogueQuip);
        }
    }

    public event Action OnDeckSizeChange;
    public void DeckSizeChange()
    {
        if (OnDeckSizeChange != null)
        {
            OnDeckSizeChange();
        }
    }



}
