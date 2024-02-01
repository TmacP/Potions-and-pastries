using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject draggableItemPrefab;
    [SerializeField] private GameObject mainInventoryGroup;
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

        UpdateInfoPanel(selectedSlot);
    }

    void UpdateInfoPanel(int slotIndex)
    {
        DraggableItem itemInSlot = inventorySlots[slotIndex].GetComponentInChildren<DraggableItem>();
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();

        if (itemInSlot != null && infoPanel != null)
        {
            infoPanel.SetInfo(itemInSlot.ItemData.name, itemInSlot.ItemData.Description, itemInSlot.ItemData.image);
        }
        else if (infoPanel != null)
        {
            infoPanel.ClearInfo();
        }
    }


    private void Start()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.inventoryManager = this;
        }

        
    }

    public void ChangeSelectedSlotBasedOnSlot(InventorySlot slot)
    {
        int slotIndex = Array.IndexOf(inventorySlots, slot);
        if (slotIndex != -1)
        {
            ChangeSelectedSlot(slotIndex);
        }
    }

    private bool IsInventoryOpen()
    {
        bool isOpen = mainInventoryGroup != null && mainInventoryGroup.activeSelf;
        //Debug.Log("IsInventoryOpen: " + isOpen + ", mainInventoryGroup: " + mainInventoryGroup);
        return isOpen;
    }


    private int slotsPerRow = 7; // inventory layout

    private void Update()
    {
        if (IsInventoryOpen())
        {
            int currentRow = selectedSlot / slotsPerRow;
            int currentColumn = selectedSlot % slotsPerRow;

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentColumn < slotsPerRow - 1)
                {
                    ChangeSelectedSlot(selectedSlot + 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentColumn > 0)
                {
                    ChangeSelectedSlot(selectedSlot - 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selectedSlot >= 7) // Assuming main inventory starts from index 7
                {
                    ChangeSelectedSlot(selectedSlot - slotsPerRow);
                }
                else
                {
                    // Special case for navigating from toolbar to main inventory
                    ChangeSelectedSlot(7 + currentColumn); // Adjusted based on layout
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentRow < (inventorySlots.Length / slotsPerRow) - 1)
                {
                    ChangeSelectedSlot(selectedSlot + slotsPerRow);
                }
                else
                {
                    // Loop back to the toolbar when at the bottom of the inventory
                    ChangeSelectedSlot(currentColumn); // This will select the corresponding toolbar slot
                }
            }
        }
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (number > 0 && isNumber && number < 8)
            {
                ChangeSelectedSlot((int)number - 1);
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
