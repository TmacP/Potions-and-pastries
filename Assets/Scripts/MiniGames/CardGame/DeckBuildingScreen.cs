using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildingScreen : MonoBehaviour
{
    public InventoryManager PlayerInventoryManager;
    public InventoryManager DeckInventoryManager;
    public InventoryManager ActionInventoryManager;

    public List<InventoryItemData> ActionInventory;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnCloseMenu += OnCloseInventory;
        PlayerInventoryManager.InitializeInventoryManager(GameManager.Instance.PlayerState.Inventory);
        DeckInventoryManager.InitializeInventoryManager(GameManager.Instance.PlayerState.Deck);
        ActionInventoryManager.InitializeInventoryManager(ActionInventory);

        PlayerInventoryManager.gameObject.SetActive(true);
        DeckInventoryManager.gameObject.SetActive(true);
        ActionInventoryManager.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnCloseMenu -= OnCloseInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCloseInventory()
    {
        Destroy(this.gameObject);
    }

    public void StartService()
    {
        if(GameManager.Instance.GetGameScene() == EGameScene.InnInterior)
        {
            GameManager.Instance.ChangeGameState(EGameState.NightState);
            Debug.Log(GameManager.Instance.GetGameState());
            GameEventManager.instance.CloseMenu();
        }
    }
}
