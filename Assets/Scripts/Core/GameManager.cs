using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum EGameScene
{
    InnInterior,
    InnExterior,
    Tutorial,
    ChangingRoom,
}



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private string saveFilePath = "saveData.txt"; //saves in the root of the project folder

    //We store this here so it persists between levels
    public PlayerStateData PlayerState;
    public int GameDay; // inkle needs to know what day it

    private EGameState GameState;
    public GameStateData PersistantGameState;
    private EGameScene GameScene = EGameScene.InnExterior;

    [SerializeField] LoadingScreen LoadUI;

    [SerializeField]
    private readonly Dictionary<EGameScene, string> GameScenes = new Dictionary<EGameScene, string>()
    {
        {EGameScene.InnInterior, "AlphaInterior" },
        {EGameScene.InnExterior, "AlphaExterior" },
        {EGameScene.Tutorial, "Tutorial" },
        {EGameScene.ChangingRoom, "ChangingRoom" }
    };

    // Structure to hold the data to be saved
    [System.Serializable]
    private class SaveData
    {
        public long goldAmount;
        public List<InventoryItemData> inventory;
        //public List<InventoryItemData> toolBar;
        public List<InventoryItemData> deck;
        //public List<InventoryItemData> cardHand;
        //public List<InventoryItemData> discard;
        //public List<fCharacterSpriteAssetData> spriteAssetData;

        public List<EGameRegion> unlockedRegions;
        public List<int> openedDoors;
        public int roomsUnlocked;
        //public List<RecipeData> pinnedRecipes;
    }

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
            //clearSave();
        }
    }

    public void clearSave(){
        PlayerState.Gold = 0;
        PlayerState.Inventory.Clear();
        PlayerState.ToolBar.Clear();
        PlayerState.CardHand.Clear();
        PlayerState.Deck.Clear();
        PlayerState.Discard.Clear();
        PersistantGameState.UnlockedRegions.Clear();
        PersistantGameState.OpenedDoors.Clear();
        PersistantGameState.RoomsUnlocked = 2;
        PersistantGameState.PinnedRecipes.Clear();
    }



    // Save the game data to a file
    public void SaveGame()
    {
        // Create a data object to hold necessary game data
        SaveData saveData = new SaveData();

        //Player State
        saveData.goldAmount = PlayerState.Gold;
        saveData.inventory = PlayerState.Inventory;
        //saveData.toolBar = PlayerState.ToolBar;
        saveData.deck = PlayerState.Deck;
        //saveData.cardHand = PlayerState.CardHand;
        //saveData.discard = PlayerState.Discard;
        //saveData.spriteAssetData = PlayerState.SpriteAssetData;


        // Persistent Game State
        saveData.unlockedRegions = PersistantGameState.UnlockedRegions;
        saveData.openedDoors = PersistantGameState.OpenedDoors;
        saveData.roomsUnlocked = PersistantGameState.RoomsUnlocked;
        //saveData.pinnedRecipes = PersistantGameState.PinnedRecipes;

        // Turn into JSON data
        string jsonData = JsonUtility.ToJson(saveData);

        // Write Json to file
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("Game saved successfully.");
    }

    public void LoadGame()
    {
        // Check if the save file exists
        if (File.Exists(saveFilePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
             
            // Player State
            PlayerState.Gold = saveData.goldAmount;
            PlayerState.Inventory = saveData.inventory;
           // PlayerState.ToolBar = saveData.toolBar;
            PlayerState.Deck = saveData.deck;
           // PlayerState.CardHand = saveData.cardHand;
           // PlayerState.Discard = saveData.discard;
           // PlayerState.SpriteAssetData = saveData.spriteAssetData;

            // Persistent Game State
            PersistantGameState.UnlockedRegions = saveData.unlockedRegions;
            PersistantGameState.OpenedDoors = saveData.openedDoors;
            PersistantGameState.RoomsUnlocked = saveData.roomsUnlocked;
           // PersistantGameState.PinnedRecipes = saveData.pinnedRecipes;

            Debug.Log("Game loaded");

            Debug.Log("Gold Amount: " + PlayerState.Gold);
            Debug.Log("Inventory Count: " + PlayerState.Inventory.Count);

            // Reload the current scene
            GameManager.Instance.ChangeGameScene(EGameScene.InnExterior);
        }
        else
        {
            Debug.LogWarning("No save file");
            GameManager.Instance.clearSave();
            GameManager.Instance.ChangeGameScene(EGameScene.Tutorial);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnUnlockRegion += OnUnlockRegion;
        GameEventManager.instance.OnDoorUnlocked += OnDoorUnlock;
        GameEventManager.instance.OnPinRecipe += OnRecipePinned;

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
        GameEventManager.instance.OnPinRecipe -= OnRecipePinned;

    }

    public void ChangeGameState(EGameState NewGameState)
    {
        if(GameState == NewGameState) { return; }

        switch (NewGameState) 
        {
            case EGameState.MainState:
                Time.timeScale = 1.0f;
                if (GameState == EGameState.NightState)
                {
                    GameDay++;
                }
                break;
            case EGameState.NightState:
                Time.timeScale = 1.0f; 
                break;
            case EGameState.PauseState:
                Time.timeScale = 0.0f;
                break;
            case EGameState.MovementDisabledState:
                Time.timeScale = 1.0f;
                break;
            case EGameState.PlayerCustomizationState:
                Time.timeScale = 1.0f;
                break;
            default:
                Debug.Log("Gamemanager::ChangeGameState unknown game state given");
                break;
        }
        EGameState OldState = GameState;
        GameState = NewGameState;
        GameEventManager.instance.ChangeGameState(GameState, OldState);
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
            case EGameScene.Tutorial:
                ChangeGameState(EGameState.MainState);
                break;
            case EGameScene.ChangingRoom:
                ChangeGameState(EGameState.PlayerCustomizationState);
                break;
            default:
                Debug.Log("Gamemanager::ChangeGameScene unkown game scene given");
                break;
        }
        bool FoundGameScene = GameScenes.TryGetValue(NewScene, out SceneName);
        if (FoundGameScene)
        {
            if(LoadUI != null)
                LoadUI.gameObject.SetActive(true);
            StartCoroutine(LoadLevel(SceneName));
        }
    }

    IEnumerator LoadLevel(string SceneName)
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(SceneName);

        while(!loadLevel.isDone)
        {
            if(LoadUI != null)
            {
                LoadUI.UpdateBar(loadLevel.progress);
            }
            yield return null;
        }
        if (LoadUI != null)
            LoadUI.gameObject.SetActive(false);
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

    public void OnRecipePinned(RecipeData Recipe)
    { 
        if(Recipe != null)
        {
            int index = PersistantGameState.PinnedRecipes.IndexOf(Recipe);
            if (index >= 0)
            {
                PersistantGameState.PinnedRecipes.RemoveAt(index);
            }
            else
            {
                PersistantGameState.PinnedRecipes.Add(Recipe);
            }
        }
        GameEventManager.instance.UpdatePostedRecipesUI();
    }
}
