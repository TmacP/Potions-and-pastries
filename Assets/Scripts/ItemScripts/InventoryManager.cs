using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject draggableItemPrefab;
    //This should be set to something external from the inventory manager - the data and UI should be seperate
    private List<InventoryItemData> InventoryDataRef; 

    [SerializeField] private GameObject mainInventoryGroup;
    public int maxStack = 5;

    int selectedSlot = -1;


    public void InitializeInventoryManager(List<InventoryItemData> InventoryData)
    {
        InventoryDataRef = InventoryData;
        ClearInventory();
        Debug.Log("******LOGGING INVENTORY MANAGER DATA**********");
        foreach(InventoryItemData InvItem in InventoryDataRef)
        {
            Debug.Log(InvItem);
            AddItemAtIndex_InternalInitialize(InvItem);
        }
    }

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
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>(true);
        if (infoPanel != null)
        {
            infoPanel.gameObject.SetActive(true);
        }

        if (itemInSlot != null && infoPanel != null)
        {
            infoPanel.SetInfo(itemInSlot.ItemData.Data.name, itemInSlot.ItemData.Data.Description, itemInSlot.ItemData.Data.image);
        }
        else if (infoPanel != null)
        {
            infoPanel.ClearInfo();
        }
    }

    private void Awake()
    {
        Array.Clear(inventorySlots, 0, inventorySlots.Length);
        inventorySlots = GetComponentsInChildren<InventorySlot>(true);
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        if(infoPanel != null)
        {
            infoPanel.gameObject.SetActive(false);
        }
        foreach (InventorySlot slot in GetComponentsInChildren<InventorySlot>(true))
        {
            slot.inventoryManager = this;
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        if (infoPanel != null)
        {
            infoPanel.gameObject.SetActive(false);
        }
    }

    public void ClearInventory()
    {
        foreach(InventorySlot slot in inventorySlots)
        {
            DraggableItem Item = slot.GetComponent<DraggableItem>();
            if(Item != null)
            {
                Destroy(Item.gameObject);
            }
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
            if (itemInSlot != null && itemInSlot.ItemData.Data == item && itemInSlot.count < maxStack && itemInSlot.ItemData.Data.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.ItemData.CurrentStackCount++;
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
                int Index = Array.IndexOf(inventorySlots, slot);
                int StackCount = 1;
                InventoryItemData InvData = new InventoryItemData(item, Index, StackCount);
                InventoryDataRef.Add(InvData);
                SpawnNewItem(InvData, slot);
                return true;
            }
        }
        return false;
    }

    private bool AddItemAtIndex_InternalInitialize(InventoryItemData InvItem)
    {
        //Find Slot
        if(InvItem.InventoryIndex >= inventorySlots.Length || InvItem.InventoryIndex < 0)
        {
            Debug.Log("InventoryManager::AddItemAtIndex... Index exceeds Slot bounds");
            return false;
        }

        InventorySlot slot = inventorySlots[InvItem.InventoryIndex];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
        if(itemInSlot != null)
        {
            if (itemInSlot.ItemData.Data == InvItem.Data && itemInSlot.count < maxStack && itemInSlot.ItemData.Data.stackable == true)
            {
                itemInSlot.count += InvItem.CurrentStackCount;
                itemInSlot.RefreshCount();
                return true;
            }
            else
            {
                Debug.Log("InventoryManager::AddItemAtIndex... Item stack exceeds max stackable");
                return false;
            }

        }
        else
        {
            SpawnNewItem(InvItem, slot);
            return true;
        }
    }

    public ItemData GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

        if(itemInSlot != null)
        {
            ItemData itemData = itemInSlot.ItemData.Data;
            if (use)
            {
                itemInSlot.count--;
                itemInSlot.ItemData.CurrentStackCount--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                    InventoryDataRef.Remove(itemInSlot.ItemData);
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



    void SpawnNewItem(InventoryItemData item, InventorySlot slot)
    {

        GameObject newItemGo = Instantiate(draggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItemGo.GetComponent<DraggableItem>();
        draggableItem.InitialiseItemData(item, this);
    }

}
