using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CycleHandScript : MonoBehaviour, IInteractable
{
    public GameObject MiniGame;
    public GameObject VFX;

    public void Start()
    {
        GameEventManager.instance.OnChangeGameState += OnChangeGameState;
        VFX.SetActive(false);
    }

    public string InteractionPrompt => GetInteractionPrompt();

    public string GetInteractionPrompt()
    {
        if(GameManager.Instance.GetGameState() == EGameState.NightState)
        {
            return "Cycle Hand";
        }
        else
        {
            return "";
        }
    }
    
    // Start is called before the first frame update
    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {
        if(GameManager.Instance.GetGameState() != EGameState.NightState)
        {
            return EInteractionResult.Failure;
        }
        if (MiniGame != null)
        {
            GameEventManager.instance.OnMiniGameComplete += OnMiniGameComplete;
            //******Start MiniGame

            //This should probably be handled in the minigame
            GameManager.Instance.ChangeGameState(EGameState.MovementDisabledState);

            Instantiate(MiniGame);
        }
        return EInteractionResult.Success;
    }
    //************ End of IInteractable ************

    public void OnMiniGameComplete(EMiniGameCompleteResult Result)
    {
        if (Result == EMiniGameCompleteResult.CriticalSuccess || Result == EMiniGameCompleteResult.Success)
        {
            OnInteractionFinished();
        }

        GameManager.Instance.ChangeGameState(EGameState.NightState);
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }

    void OnDisable()
    {
        GameEventManager.instance.OnChangeGameState -= OnChangeGameState;
        GameEventManager.instance.OnMiniGameComplete -= OnMiniGameComplete;
    }

    void OnInteractionFinished()
    {
        DeckManager DM = PlayerController.instance.GetComponent<DeckManager>();
        Assert.IsNotNull(DM);
        CardHandManager CM = FindObjectOfType<CardHandManager>();
        Assert.IsNotNull(CM);

        int handSize = CM.DiscardHand();
        for (int i = 0; i < handSize; i++)
        {
            CM.DrawCard();
        }
    }

    public void OnChangeGameState(EGameState NewState,  EGameState OldState)
    {
        if(NewState == EGameState.NightState)
        {
            VFX.SetActive(true);
        }
    }
}
