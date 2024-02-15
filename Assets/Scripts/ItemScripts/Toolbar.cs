using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public InventoryManager ToolbarManager;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnNPCRecieveOrder += OnRecieveOrder;
        PlayerController.instance.toolbar = this;
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnNPCRecieveOrder -= OnRecieveOrder;
    }

    public void OnRecieveOrder()
    {
        ToolbarManager.ClearInventory();
        ToolbarManager.InventoryDataRef.Clear();
    }

    public List<InventoryItemData> GetItems()
    {
        return ToolbarManager.InventoryDataRef;
    }
}
