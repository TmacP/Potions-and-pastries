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

                if(Scroll.y > 0.1)
                {
                    ToolbarManager.ChangeSelectedSlot(1);
                }
                else if(Scroll.y < -0.1)
                {
                    ToolbarManager.ChangeSelectedSlot(-1);
                }

            }
        }
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

    public InventoryItemData GetSelectedItem()
    {
        return ToolbarManager.GetSelectedItem(false);
    }

    public bool UseSelectedItem()
    {
        return ToolbarManager.UseItem(ToolbarManager.selectedSlot);
    }
}
