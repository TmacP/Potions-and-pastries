using JetBrains.Annotations;
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


    private void Update()
    {
        if(PlayerController.instance._PlayerActions != null)
        {
            if(PlayerController.instance._PlayerActions.PlayerActionMap.enabled)
            {
                Vector2 Scroll = PlayerController.instance._PlayerActions.PlayerActionMap.ToolbarScroll.ReadValue<Vector2>();
                UpdateScrollSlot(Scroll.y);
            }
        }
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnNPCRecieveOrder -= OnRecieveOrder;
    }

    public void OnRecieveOrder()
    {
        //ToolbarManager.ClearInventory();
        //ToolbarManager.InventoryDataRef.Clear();
    }

    public List<InventoryItemData> GetItems()
    {
        return ToolbarManager.InventoryDataRef;
    }

    public InventoryItemData GetSelectedItem()
    {
        return ToolbarManager.GetSelectedItem(false);
    }

    virtual public bool UseSelectedItem()
    {
        Debug.Log("Item Used");
        return ToolbarManager.UseItem(ToolbarManager.selectedSlot);
    }


    virtual public void UpdateScrollSlot(float ScrollValue)
    {
        if (ScrollValue > 0.1)
        {
            ToolbarManager.ChangeSelectedSlot(1);
        }
        else if (ScrollValue < -0.1)
        {
            ToolbarManager.ChangeSelectedSlot(-1);
        }
    }
}
