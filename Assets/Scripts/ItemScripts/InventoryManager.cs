using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject draggableItemPrefab;
    //This should be set to something external from the inventory manager - the data and UI should be seperate
    public List<InventoryItemData> InventoryDataRef;

    [SerializeField] private GameObject mainInventoryGroup;
    public int maxStack = 5;

    public int selectedSlot = -1;
    private bool bIsCardInventory = false;

    public bool CloseOnCloseMenuEvent = true;
    public bool bIsMainMenu = true;
    private int slotsPerRow = 4; // inventory layout



    public virtual void InitializeInventoryManager(List<InventoryItemData> InventoryData)
    {
        InventoryDataRef = InventoryData;
        RefreshInventory();
    }

    public void SelectSlotBasedOnItem(DraggableItem item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].GetComponentInChildren<DraggableItem>(true) == item)
            {
                SetSelectedSlot(i);
                break;
            }
        }
    }


    public void SetSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;

        UpdateInfoPanel(selectedSlot);
    }

    public void ChangeSelectedSlot(int DeltaSlot)
    {
        int NewSlot = selectedSlot + DeltaSlot;

        if(NewSlot >= 0 && NewSlot < inventorySlots.Length)
        {
            SetSelectedSlot(NewSlot);
        }
    }

    void UpdateInfoPanel(int slotIndex)
    {
        DraggableItem itemInSlot = inventorySlots[slotIndex].GetComponentInChildren<DraggableItem>(true);
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>(true);
        if (infoPanel != null)
        {
            infoPanel.gameObject.SetActive(true);
        }

        if (itemInSlot != null && infoPanel != null)
        {
            infoPanel.SetInfo(itemInSlot.ItemData.Data.name, itemInSlot.ItemData.Data.Description, itemInSlot.ItemData.CurrentItemTags, itemInSlot.ItemData.Data.image);
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

    public void OnEnable()
    {
        GameEventManager.instance.OnCloseMenu += CloseInventory;
        GameEventManager.instance.OnRefreshInventory += RefreshInventory;
        if(bIsMainMenu)
        {
            GameEventManager.instance.PostInventoryOpen();
        }
        RefreshInventory();
    }

    public virtual void OnDisable()
    {
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        if (infoPanel != null)
        {
            infoPanel.gameObject.SetActive(false);
        }
        GameEventManager.instance.OnCloseMenu -= CloseInventory;
        GameEventManager.instance.OnRefreshInventory -= RefreshInventory;
    }

    public virtual void RefreshInventory()
    {
        ClearInventory();
        if (InventoryDataRef != null)
        {
            foreach (InventoryItemData InvItem in InventoryDataRef)
            {
                AddItemToUI(InvItem);
            }
        }
        else
        {
            //Debug.Log(name + "No InventoryData");
        }
    }

    public void ClearInventory()
    {
        if(IsInventoryOpen() == true)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if(slot != null)
                {
                    DraggableItem Item = slot.GetComponentInChildren<DraggableItem>(true);
                    if (Item != null)
                    {
                        DestroyImmediate(Item.gameObject);
                    }
                }
            }
        }
        
    }

    private void OnDestroy()
    {
        InventoryDataRef = null;
    }

    public void ChangeSelectedSlotBasedOnSlot(InventorySlot slot)
    {
        int slotIndex = Array.IndexOf(inventorySlots, slot);
        if (slotIndex != -1)
        {
            SetSelectedSlot(slotIndex);
        }
    }

    private bool IsInventoryOpen()
    {
        bool isOpen = mainInventoryGroup != null && mainInventoryGroup.activeSelf;
        //Debug.Log("IsInventoryOpen: " + isOpen + ", mainInventoryGroup: " + mainInventoryGroup);
        return isOpen;
    }



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
                    SetSelectedSlot(selectedSlot + 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentColumn > 0)
                {
                    SetSelectedSlot(selectedSlot - 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selectedSlot >= 7) // Assuming main inventory starts from index 7
                {
                    SetSelectedSlot(selectedSlot - slotsPerRow);
                }
                else
                {
                    // Special case for navigating from toolbar to main inventory
                    SetSelectedSlot(7 + currentColumn); // Adjusted based on layout
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentRow < (inventorySlots.Length / slotsPerRow) - 1)
                {
                    SetSelectedSlot(selectedSlot + slotsPerRow);
                }
                else
                {
                    // Loop back to the toolbar when at the bottom of the inventory
                    SetSelectedSlot(currentColumn); // This will select the corresponding toolbar slot
                }
            }
        }
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (number > 0 && isNumber && number < 8)
            {
                SetSelectedSlot((int)number - 1);
            }
        }
    }


    public bool AddItem(InventoryItemData item)
    {
        InventoryDataRef.Sort((I1, I2) => I1.InventoryIndex.CompareTo(I2.InventoryIndex));
        int smallestIndex = 0;

        if (item.CurrentStackCount < 1)
        {
            item.CurrentStackCount = 1;
        }

        // Find an item is re-occuring: add it to stack
        foreach (InventoryItemData InvData in InventoryDataRef)
        {
            if(InventoryItemData.IsEquivalent(InvData,item))
            {
                if (InvData.Data.stackable && InvData.CurrentStackCount < maxStack)
                {
                    //This is a bug since we can get stacks bigger than 5
                    InvData.CurrentStackCount+= item.CurrentStackCount;

                    if(InvData.CurrentStackCount > maxStack)
                    {
                        int Excess = InvData.CurrentStackCount - maxStack;
                        InventoryItemData ExcessItem = new InventoryItemData(item.Data, -1, Excess);
                        AddItem(ExcessItem);
                    }

                    DraggableItem itemInSlot = inventorySlots[InvData.InventoryIndex].GetComponent<DraggableItem>();
                    if(itemInSlot != null)
                    {
                        itemInSlot.RefreshCount();
                    }
                    return true;
                }
            }
            if(smallestIndex == InvData.InventoryIndex)
            {
                smallestIndex++;
            }
        }

        if(smallestIndex >= inventorySlots.Length)
        {
            return false;
        }

        item.InventoryIndex = smallestIndex;
        return AddItemAtIndex(item, smallestIndex, false);

    }

    public virtual bool AddItemAtIndex(InventoryItemData InvItem, int Index, bool UpdateGameLog = true)
    {
        //Find Slot
        Assert.IsFalse(Index >= inventorySlots.Length || Index < 0);
        foreach (InventoryItemData InvData in InventoryDataRef)
        {
            if (InvData.InventoryIndex == Index )
            {
                InvItem.bIsCard = bIsCardInventory;
                if (InventoryItemData.IsEquivalent(InvItem, InvData) && InvData.Data.stackable && InvData.CurrentStackCount < maxStack)
                {
                    InvData.CurrentStackCount++; //InvItem.CurrentStackCount;

                    DraggableItem itemInSlot = inventorySlots[InvData.InventoryIndex].GetComponent<DraggableItem>();
                    if (itemInSlot != null)
                    {
                        itemInSlot.RefreshCount();
                    }
                    GameEventManager.instance.RefreshInventory();
                    return true;
                }
                else
                {
                    Debug.Log("InventoryManager::AddItemAtIndex... Index is full");
                    return false;
                }
            }
        }

        InvItem.InventoryIndex = Index;
        InventoryDataRef.Add(InvItem);
        AddItemToUI(InvItem);
        GameEventManager.instance.RefreshInventory();
        return true;
        
    }

    private void AddItemToUI(InventoryItemData InvItem)
    {
        Assert.IsFalse(InvItem.InventoryIndex >= inventorySlots.Length || InvItem.InventoryIndex < 0);
        InventorySlot slot = inventorySlots[InvItem.InventoryIndex];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>(true);
        if (itemInSlot != null)
        {
            if (itemInSlot.ItemData.Data == InvItem.Data && itemInSlot.count < maxStack && itemInSlot.ItemData.Data.stackable == true)
            {
                itemInSlot.count += InvItem.CurrentStackCount;
                itemInSlot.RefreshCount();
                return;
            }
            else
            {
                return;
            }
        }
        else
        {
            SpawnNewItem(InvItem, slot);
            return;
        }
    }

    public virtual InventoryItemData RemoveItem(InventoryItemData InvItem, bool RemoveEntireStack = false)
    {
        int Index = InvItem.InventoryIndex;

        //These checks could be cut at somepoint
        Assert.IsFalse(Index < 0 || Index >= inventorySlots.Length);
        Assert.IsNotNull(InventoryDataRef);
        Assert.IsNotNull(InvItem);

        InventorySlot slot = inventorySlots[Index];
        if (slot != null)
        {
            DraggableItem ItemInSlot = slot.transform.GetComponentInChildren<DraggableItem>(true);
            if(RemoveEntireStack)
            {
                bool SuccessfulRemoval = InventoryDataRef.Remove(InvItem);
                if (ItemInSlot != null)
                {
                    Destroy(ItemInSlot.gameObject);
                }
                return InvItem;
            }

            InvItem.CurrentStackCount--;
            if (RemoveEntireStack || InvItem.CurrentStackCount <= 0 )
            {
                bool SuccessfulRemoval = InventoryDataRef.Remove(InvItem);
                if (ItemInSlot != null)
                {
                    Destroy(ItemInSlot.gameObject);
                }
            }
            else
            {
                Debug.Log("Refreshing Count: " + InvItem.CurrentStackCount);
                ItemInSlot.RefreshCount();
            }
            return InvItem;
        }
        return null;
    }

    public InventoryItemData GetItemByType(ItemData item)
    {
        return InventoryDataRef.FirstOrDefault(i => i.Data == item);
    }
    public InventoryItemData GetSelectedItem(bool use)
    {
        if(selectedSlot < 0 || selectedSlot >= inventorySlots.Length)
            return null;

        InventorySlot slot = inventorySlots[selectedSlot];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>(true);

        if(itemInSlot != null)
        {
            InventoryItemData itemData = itemInSlot.ItemData;
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

    public InventorySlot GetSlotByIndex(int SlotIndex)
    {
        if(SlotIndex >= 0 && SlotIndex < inventorySlots.Length)
        {
            return inventorySlots[SlotIndex];
        }
        return null;
    }

    public bool UseItem(int ItemIndex)
    {
        InventorySlot slot = inventorySlots[ItemIndex];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>(true);

        if (itemInSlot != null)
        {
            InventoryItemData itemData = itemInSlot.ItemData;
            RemoveItem(itemData);
            itemInSlot.count--;
            itemInSlot.ItemData.CurrentStackCount--;
            if (itemInSlot.count <= 0)
            {
                RemoveItem(itemData);
                RefreshInventory();
            }
            else
            {
                itemInSlot.RefreshCount();
            }
            return true;
        }
        return false;
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


