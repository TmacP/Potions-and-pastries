using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Linq;

public class GatherableBehavoir : MonoBehaviour, IInteractable
{
    [SerializeField] private GatherableData Data;
    [SerializeField] private bool DestroyOnEmpty = true;

//********IInteractable Interface **********
    public string InteractionPrompt => Data.InteractionPrompt;

    public bool TryInteract(InteractorBehavoir InInteractor)
    { 
        if(Data.CollectableItems.Count > 0)
        {
            if (Data.MiniGame != null)
            {
                GameEventManager.instance.OnMiniGameComplete += OnMiniGameComplete;
                //******Start MiniGame

                //This should probably be handled in the minigame
                GameManager.Instance.ChangeGameState(EGameState.MovementDisabledState);

                Data.MiniGame.InstantiateAsync(Vector3.zero, Quaternion.identity).WaitForCompletion();
            }
            else
            {
                List<InventoryItemData> Items = new List<InventoryItemData>();
                GetItemsToGive(out Items);
                GameEventManager.instance.GivePlayerItems(Items);
            }
            return true;
        }
        
        return false;
    }
//************ End of IInteractable ************

    public void OnMiniGameComplete(EMiniGameCompleteResult Result)
    {
        if(Result == EMiniGameCompleteResult.CriticalSuccess)
        {
            List<InventoryItemData> Items = new List<InventoryItemData>();
            GetItemsToGive(out Items);
            Items.AddRange(Items);

            Debug.Log("Gained 2x Item");
            GameEventManager.instance.GivePlayerItems(Items);
            Data.NumberOfInteractions--;
            OnInteractionFinished();
            
        }
        else if(Result == EMiniGameCompleteResult.Success)
        {
            List<InventoryItemData> Items = new List<InventoryItemData>();
            GetItemsToGive(out Items);

            GameEventManager.instance.GivePlayerItems(Items);

            Data.NumberOfInteractions--;
            OnInteractionFinished();

        }
        
        GameManager.Instance.ChangeGameState(EGameState.MainState);
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }

    void OnDisable()
    {
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }


    void GetItemsToGive(out List<InventoryItemData> OutItems)
    {
        OutItems = new List<InventoryItemData>();

        foreach(ItemData ItemData in Data.CollectableItems)
        {
            InventoryItemData InvItem = new InventoryItemData(ItemData, -1, -1);
            OutItems.Add(InvItem);
        }
    }

    void OnInteractionFinished()
    {
        if (Data.NumberOfInteractions <= 0)
        {
            if (DestroyOnEmpty)
            {
                Destroy(this.gameObject);
            }
            else
            {
                int LayerIndex = LayerMask.NameToLayer("Interact");
                this.gameObject.layer &= (0x1 << LayerIndex);
            }
        }
    }
}
