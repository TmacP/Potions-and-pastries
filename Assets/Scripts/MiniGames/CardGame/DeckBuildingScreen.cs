using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DeckBuildingScreen : MonoBehaviour
{
    public InventoryManager PlayerInventoryManager;
    public InventoryManager DeckInventoryManager;
    public InventoryManager ActionInventoryManager;
    public GameObject StartServiceButton;
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

        if (GameManager.Instance.GetGameScene() == EGameScene.InnInterior || GameManager.Instance.GetGameScene() == EGameScene.Tutorial)
        {
            Assert.IsNotNull(StartServiceButton);
            StartServiceButton.SetActive(true);
        }
        else
        {
            Assert.IsNotNull(StartServiceButton);
            StartServiceButton.SetActive(false);
        }

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
        if((GameManager.Instance.GetGameScene() == EGameScene.InnInterior) || (GameManager.Instance.GetGameScene() == EGameScene.Tutorial))
        {
            GameManager.Instance.ChangeGameState(EGameState.NightState);
            GameEventManager.instance.CloseMenu();
        }
        else
        {

        }
    }
}
