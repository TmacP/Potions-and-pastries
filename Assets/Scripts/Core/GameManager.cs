using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState
{
    MainState,
    PauseState,
    MovementDisabledState
}

public enum EGameScene
{
    InnInterior,
    InnExterior,
    ConorInnInterior,
    ConorInnExterior,
    AlphaInterior,
    AlphaExterior
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //We store this here so it persists between levels
    public PlayerStateData PlayerState;
    
    [SerializeField]
    private readonly Dictionary<EGameScene, string> GameScenes = new Dictionary<EGameScene, string>()
    {
        {EGameScene.InnInterior, "ConorInnScene" },
        {EGameScene.InnExterior, "ConorDemoScene" },
        {EGameScene.ConorInnInterior, "ConorInnScene" },
        {EGameScene.ConorInnExterior, "ConorDemoScene" },
        {EGameScene.AlphaInterior, "Assets/Scenes/Alpha/AlphaInterior.unity" },
        {EGameScene.AlphaExterior, "Assets/Scenes/Alpha/AlphaExterior.unity" },
    };

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            PlayerState.Inventory.Clear();
            PlayerState.ToolBar.Clear();
        }
    }


    private EGameState GameState;
    private EGameScene GameScene;

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

    public EGameState GetGameState()
    {
        return GameState;
    }

    public EGameScene GetGameScene()
    {
        return GameScene;
    }

    public void ChangeGameScene(EGameScene NewScene)
    {
        string SceneName;
        GameScene = NewScene;

        //This is to handle any special cases before we change scenes
        switch (NewScene)
        {
            case EGameScene.InnInterior:
                break;
            case EGameScene.InnExterior:
                break;
            default:
                Debug.Log("Gamemanager::ChangeGameScene unkown game scene given");
                break;
        }
        bool FoundGameScene = GameScenes.TryGetValue(NewScene, out SceneName);
        if (FoundGameScene)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
