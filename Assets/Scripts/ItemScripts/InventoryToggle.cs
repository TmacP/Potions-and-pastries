using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject mainInventoryGroup; // Assign this in the inspector
    public InventoryManager InventoryManager; // Assign this in the inspector 

    private void Start()
    {
        //GameEventManager.instance.OnToggleInventory += Toggle;
        GameEventManager.instance.OnGivePlayerItems += GainItems;
        mainInventoryGroup.SetActive(false);
    }

    private void OnDisable()
    {
        //GameEventManager.instance.OnToggleInventory -= Toggle;
        GameEventManager.instance.OnGivePlayerItems -= GainItems;
    }


    public bool Toggle()
    {
        mainInventoryGroup.SetActive(!mainInventoryGroup.activeSelf);
        if(mainInventoryGroup.activeSelf)
        {
            GameEventManager.instance.InventoryOpen(InventoryManager);
            return true;
        }
        else
        {
            //GameEventManager.instance.InventoryClosed(InventoryManager);
            return false;
        }
    }

    void GainItems(List<ItemData> items)
    {
        foreach (ItemData item in items)
        {
            InventoryManager.AddItem(item);
        }
    }
}
