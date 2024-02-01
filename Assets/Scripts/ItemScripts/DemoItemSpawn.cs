using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoItemSpawn : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData[] itemsToPickup;


    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Item added...");
        }
        else
        {
            Debug.Log("Item not added.");
        }
    }

    public void GetSelectedItem()
    {
        ItemData receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Recieved item:" + receivedItem);
        }
        else { Debug.Log("No item received."); }
    }

    public void UseSelectedItem()
    {
        ItemData receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item:" + receivedItem);
        }
        else { Debug.Log("No item used."); }
    }
}
