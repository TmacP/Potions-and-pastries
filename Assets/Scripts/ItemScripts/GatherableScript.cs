using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

public class GatherableBehavoir : MonoBehaviour, IInteractable
{
    [SerializeField] private GatherableData Data;

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
                List<ItemData> Items = new List<ItemData>();
                GetItemsToGive(out Items);
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
            List<ItemData> Items = new List<ItemData>();
            GetItemsToGive(out Items);

            Debug.Log("Gained 2x Item");
            //GameManager.Instance.GainItem();
            Data.NumberOfInteractions--;
            if (Data.NumberOfInteractions <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else if(Result == EMiniGameCompleteResult.Success)
        {
            List<ItemData> Items = new List<ItemData>();
            GetItemsToGive(out Items);

            Debug.Log("Gained 1x Item");
            //GameManager.Instance.GainItem();
            Data.NumberOfInteractions--;
            if (Data.NumberOfInteractions <= 0)
            {
                Destroy(this.gameObject);
            }

        }
        
        GameManager.Instance.ChangeGameState(EGameState.MainState);
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }

    void OnDisable()
    {
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }


    void GetItemsToGive(out List<ItemData> OutItems)
    {
        OutItems = new List<ItemData>();
    }
}
