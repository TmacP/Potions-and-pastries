using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    MainState,
    PauseState,
    MovementDisabledState
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    private EGameState GameState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(EGameState NewGameState)
    {
        if(GameState == NewGameState) { return; }

        GameEventManager.instance.ChangeGameState(NewGameState, GameState);
        switch (NewGameState) 
        {
            case EGameState.MainState:
                Time.timeScale = 1.0f;
                break;
            case EGameState.PauseState:
                Time.timeScale = 0.0f;
                break;
            case EGameState.MovementDisabledState:
                Time.timeScale = 1.0f;
                break;
            default:
                Debug.Log("Gamemanager::ChangeGameState unknown game state given");
                break;
        }
        GameState = NewGameState;
    }
}
