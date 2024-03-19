using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


public enum EGameScene
{
    InnInterior,
    InnExterior,
}



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //We store this here so it persists between levels
    public PlayerStateData PlayerState;
    public int GameDay; // inkle needs to know what day it

    private EGameState GameState;
    public GameStateData PersistantGameState;
    private EGameScene GameScene;

    [SerializeField]
    private readonly Dictionary<EGameScene, string> GameScenes = new Dictionary<EGameScene, string>()
    {
        {EGameScene.InnInterior, "AlphaInterior" },
        {EGameScene.InnExterior, "AlphaExterior" }
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
            PlayerState.CardHand.Clear();
            PlayerState.Deck.Clear();
            PlayerState.Discard.Clear();
            PersistantGameState.UnlockedRegions.Clear();
            PersistantGameState.OpenedDoors.Clear();
        }
    }


    

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnUnlockRegion += OnUnlockRegion;
        GameEventManager.instance.OnDoorUnlocked += OnDoorUnlock;

        GameDay = 1; // we start on day 1 for the tutorial

        string Name = SceneManager.GetActiveScene().name;
        foreach (KeyValuePair<EGameScene, string> pair in GameScenes)
        {
            if(pair.Value == Name)
            {
                GameScene = pair.Key;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnUnlockRegion -= OnUnlockRegion;
        GameEventManager.instance.OnDoorUnlocked -= OnDoorUnlock;

    }

    public void ChangeGameState(EGameState NewGameState)
    {
        if(GameState == NewGameState) { return; }

        switch (NewGameState) 
        {
            case EGameState.MainState:
                Time.timeScale = 1.0f;
                PlayerController.instance.GetComponent<DeckManager>().OnChangeGameScene(NewGameState);
                break;
            case EGameState.NightState:
                Time.timeScale = 1.0f; 
                PlayerController.instance.GetComponent<DeckManager>().FlattenDeckInventory();
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
        GameEventManager.instance.ChangeGameState(NewGameState, GameState);
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
                ChangeGameState(EGameState.MainState);
                break;
            case EGameScene.InnExterior:
                ChangeGameState(EGameState.MainState);
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

    public void OnUnlockRegion(EGameRegion Region)
    {
        if(!PersistantGameState.UnlockedRegions.Contains(Region))
        {
            PersistantGameState.UnlockedRegions.Add(Region);
        }
    }

    public void OnDoorUnlock(int DoorID)
    {
        if (!PersistantGameState.OpenedDoors.Contains(DoorID))
        {
            PersistantGameState.OpenedDoors.Add(DoorID);
        }
    }
}
