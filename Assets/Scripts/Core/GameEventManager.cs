using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EMiniGameCompleteResult
{
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

    public event Action<EMiniGameCompleteResult> OnMiniGameComplete;
    public void MiniGameComplete(EMiniGameCompleteResult CompletionResult)
    {
        if(OnMiniGameComplete != null)
        {
            OnMiniGameComplete(CompletionResult);
        }
    }
}
