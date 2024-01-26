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
        switch (NewGameState) 
        {
        case EGameState.MainState:
            break;
        case EGameState.PauseState:
            break;
        case EGameState.MovementDisabledState:
            break;
        default:
                Debug.Log("Gamemanager::ChangeGameState unknown game state given");
                break;
        }
    }
}
