using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public InventoryManager inventoryManager;

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
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;

    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) // Check if theres nothing underneath the item
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            if (draggableItem.inventoryManager == this.inventoryManager)
            {
                draggableItem.parentAfterDrag = transform;
                draggableItem.ItemData.InventoryIndex = Array.IndexOf(inventoryManager.inventorySlots, this);
            }
            else
            {
                draggableItem.inventoryManager.RemoveItem(draggableItem.ItemData, true);
                this.inventoryManager.AddItemAtIndex(draggableItem.ItemData, Array.IndexOf(inventoryManager.inventorySlots, this));
                Destroy(draggableItem.gameObject);
            }
        }
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }


}
