using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject draggableItemPrefab;
    public int maxStack = 5;

    int selectedSlot = -1;


    public void SelectSlotBasedOnItem(DraggableItem item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].GetComponentInChildren<DraggableItem>() == item)
            {
                ChangeSelectedSlot(i);
                break;
            }
        }
    }


    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    private void Start()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.inventoryManager = this;
        }

        ChangeSelectedSlot(0);
    }

    public void ChangeSelectedSlotBasedOnSlot(InventorySlot slot)
    {
        int slotIndex = Array.IndexOf(inventorySlots, slot);
        if (slotIndex != -1)
        {
            ChangeSelectedSlot(slotIndex);
        }
    }



    private void Update() {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (number > 0 && isNumber  && number < 8)
            {
                ChangeSelectedSlot((int)number -1);
            }
        }
    }

    public bool AddItem(ItemData item)
    {

        // Find an item is re-occuring: add it to stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot != null && itemInSlot.ItemData == item && itemInSlot.count < maxStack && itemInSlot.ItemData.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Find an empty inv slot and spawn an item there
        for (int i = 0; i < inventorySlots.Length; i++) 
        { 
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public ItemData GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

        if(itemInSlot != null)
        {
            ItemData itemData = itemInSlot.ItemData;
            if (use)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return itemData;
        }
        return null;

    }



    void SpawnNewItem(ItemData item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(draggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItemGo.GetComponent<DraggableItem>();
        draggableItem.InitialiseItemData(item, this);
    }

}
