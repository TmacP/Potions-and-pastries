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
        instance = this;
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


    public event Action<EMiniGameCompleteResult> OnMiniGameComplete;
    public void MiniGameComplete(EMiniGameCompleteResult CompletionResult)
    {
        if(OnMiniGameComplete != null)
        {
            OnMiniGameComplete(CompletionResult);
        }
    }




}
