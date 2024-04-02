using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public InventoryManager inventoryManager;

    Vector3 InitialScale = Vector3.one;
    public float SelectionScaleFactor = 1.2f;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryManager.ChangeSelectedSlotBasedOnSlot(this);

            DraggableItem itemInSlot = GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null)
            {
                // Clear info if the slot is empty
                InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
                if (infoPanel != null)
                {
                    infoPanel.ClearInfo();
                }
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryManager.ChangeSelectedSlotBasedOnSlot(this);

        DraggableItem itemInSlot = GetComponentInChildren<DraggableItem>();
        if (itemInSlot == null)
        {
            // Clear info if the slot is empty
            InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
            if (infoPanel != null)
            {
                infoPanel.ClearInfo();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryManager.ChangeSelectedSlotBasedOnSlot(null);
        // Clear info if the slot is empty
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        if (infoPanel != null)
        {
            infoPanel.ClearInfo();
        }
    }

    private void Awake()
    {
        InitialScale = transform.localScale;
        Deselect();
    }
    public void Select()
    {
        this.transform.localScale = this.transform.localScale * SelectionScaleFactor;
        image.color = selectedColor;

    }

    public void Deselect()
    {
        this.transform.localScale = InitialScale;
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (transform.childCount > 0)
        {
            DraggableItem Item = GetComponentInChildren<DraggableItem>();
            Assert.IsTrue(Item != null);
            if(InventoryItemData.IsEquivalent(Item.ItemData, draggableItem.ItemData))
            {
                int NewStack = Item.ItemData.CurrentStackCount + draggableItem.ItemData.CurrentStackCount;
                if(NewStack > inventoryManager.maxStack)
                {
                    Item.ItemData.CurrentStackCount = inventoryManager.maxStack;
                    draggableItem.ItemData.CurrentStackCount = NewStack - inventoryManager.maxStack;
                }
                else
                {
                    Item.ItemData.CurrentStackCount = NewStack;
                    draggableItem.ItemData.CurrentStackCount = 0;
                }
                if(draggableItem.ItemData.CurrentStackCount <= 0)
                {
                    draggableItem.inventoryManager.RemoveItem(draggableItem.ItemData, true);
                    //this.inventoryManager.AddItemAtIndex(draggableItem.ItemData, Array.IndexOf(inventoryManager.inventorySlots, this));
                }
                Destroy(draggableItem.gameObject);
                GameEventManager.instance.RefreshInventory();
            }
        }
        else
        if (transform.childCount == 0) // Check if theres nothing underneath the item
        {
            

            if (draggableItem.inventoryManager == this.inventoryManager)
            {
                draggableItem.parentAfterDrag = transform;
                draggableItem.ItemData.InventoryIndex = inventoryManager.inventorySlots.IndexOf(this);//Array.IndexOf(inventoryManager.inventorySlots, this);
            }
            else
            {
                draggableItem.inventoryManager.RemoveItem(draggableItem.ItemData, true);
                this.inventoryManager.AddItemAtIndex(draggableItem.ItemData, inventoryManager.inventorySlots.IndexOf(this), this);
                Destroy(draggableItem.gameObject);
            }
        }
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }


}
