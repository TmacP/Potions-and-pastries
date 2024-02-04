using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject mainInventoryGroup; // Assign this in the inspector
    public InventoryManager InventoryManager; // Assign this in the inspector 

    private void Start()
    {
        GameEventManager.instance.OnToggleInventory += Toggle;
        GameEventManager.instance.OnGivePlayerItems += GainItems;
        GameEventManager.instance.OnCloseInventory += closeInventory;
        mainInventoryGroup.SetActive(false);
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnToggleInventory -= Toggle;
        GameEventManager.instance.OnGivePlayerItems -= GainItems;
        GameEventManager.instance.OnCloseInventory -= closeInventory;
    }


    void Toggle()
    {
        mainInventoryGroup.SetActive(!mainInventoryGroup.activeSelf);
    }

    void closeInventory()
    {
        mainInventoryGroup.SetActive(false);
        Debug.Log("Closed??");
    }  

    void GainItems(List<ItemData> items)
    {
        foreach (ItemData item in items)
        {
            InventoryManager.AddItem(item);
        }
    }
}
