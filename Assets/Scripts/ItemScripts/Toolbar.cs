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
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnNPCRecieveOrder -= OnRecieveOrder;
    }

    public void OnRecieveOrder()
    {
        ToolbarManager.ClearInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
