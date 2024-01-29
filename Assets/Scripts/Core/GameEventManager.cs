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


    public event Action OnCancelInteraction;
    public void CancelInteraction()
    {
        if (OnCancelInteraction != null)
        {
            OnCancelInteraction();
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

    public event Action<List<ItemData>> OnGainItem;
    public void GainItem(List<ItemData> Items)
    { 
        if(OnGainItem != null) 
        {
            OnGainItem(Items);
        }
    }




}
