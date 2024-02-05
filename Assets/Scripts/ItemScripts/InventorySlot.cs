using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
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
            draggableItem.parentAfterDrag = transform;
            draggableItem.ItemData.InventoryIndex = Array.IndexOf(inventoryManager.inventorySlots, this);
        }
    }

}
