using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject draggableItemPrefab;
    //This should be set to something external from the inventory manager - the data and UI should be seperate
    public List<InventoryItemData> InventoryDataRef; 

    [SerializeField] private GameObject mainInventoryGroup;
    public int maxStack = 5;

    int selectedSlot = -1;

    public bool CloseOnCloseMenuEvent = true;


    public virtual void InitializeInventoryManager(List<InventoryItemData> InventoryData)
    {
        InventoryDataRef = InventoryData;
        RefreshInventory();
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
        GameEventManager.instance.OnCloseMenu += CloseInventory;
        GameEventManager.instance.PostInventoryOpen();
        RefreshInventory();
    }

    private void OnDisable()
    {
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        if (infoPanel != null)
        {
            infoPanel.gameObject.SetActive(false);
        }
        ClearInventory();
        GameEventManager.instance.OnCloseMenu -= CloseInventory;
    }

    public void RefreshInventory()
    {
        ClearInventory();
        if (InventoryDataRef != null)
        {
            foreach (InventoryItemData InvItem in InventoryDataRef)
            {
                AddItemAtIndex_InternalInitialize(InvItem);
            }
        }
        else
        {
            Debug.Log(name + "No InventoryData");
        }
    }

    public void ClearInventory()
    {
        foreach(InventorySlot slot in inventorySlots)
        {
            DraggableItem Item = slot.GetComponentInChildren<DraggableItem>();
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
                RefreshInventory();
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
                RefreshInventory();
                return true;
            }
        }
        return false;
    }

    public virtual bool AddItemAtIndex(InventoryItemData InvItem, int Index)
    {
        //Find Slot
        if (Index >= inventorySlots.Length || Index < 0)
        {
            return false;
        }
        InventorySlot slot = inventorySlots[Index];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
        if (itemInSlot != null)
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
            InvItem.InventoryIndex = Index;
            int StackCount = InvItem.CurrentStackCount > 0 ? InvItem.CurrentStackCount : 1;
            InventoryDataRef.Add(InvItem);
            SpawnNewItem(InvItem, slot);
            return true;
        }
    }

    private bool AddItemAtIndex_InternalInitialize(InventoryItemData InvItem)
    {
        
        Assert.IsFalse(InvItem.InventoryIndex >= inventorySlots.Length || InvItem.InventoryIndex < 0);



        InventorySlot slot = inventorySlots[InvItem.InventoryIndex];
        Assert.IsNotNull(slot);
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
                return false;
            }

        }
        else
        {
            SpawnNewItem(InvItem, slot);
            return true;
        }
    }

    public virtual InventoryItemData RemoveItem(InventoryItemData InvItem)
    {
        int Index = InvItem.InventoryIndex;

        //These checks could be cut at somepoint
        Assert.IsFalse(Index < 0 || Index >= inventorySlots.Length);
        Assert.IsNotNull(InventoryDataRef);
        Assert.IsNotNull(InvItem);

        InventorySlot slot = inventorySlots[Index];
        if (slot != null)
        {
            DraggableItem ItemInSlot = slot.transform.GetComponentInChildren<DraggableItem>();
            bool SuccessfulRemoval = InventoryDataRef.Remove(InvItem);
            if(ItemInSlot != null)
            {
                Destroy(ItemInSlot.gameObject);
            }
            if (SuccessfulRemoval)
            {
                return InvItem;
            }
        }
        return null;
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
        Assert.IsNotNull(slot);
        GameObject newItemGo = Instantiate(draggableItemPrefab, slot.transform);
        Assert.IsNotNull(newItemGo);
        DraggableItem draggableItem = newItemGo.GetComponent<DraggableItem>();
        Assert.IsNotNull(draggableItem);
        draggableItem.InitialiseItemData(item, this);
    }

    protected virtual void CloseInventory()
    {
        if(CloseOnCloseMenuEvent)
        {
            this.gameObject.SetActive(false);
        }
    }
}


