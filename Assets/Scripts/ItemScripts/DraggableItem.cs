using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    private InfoPanel infoPanel;

    [HideInInspector] public InventoryItemData ItemData;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    [HideInInspector] public InventoryManager inventoryManager;

    public void InitialiseItemData(InventoryItemData newItem, InventoryManager manager)
    {
        ItemData = newItem;
        inventoryManager = manager;
        image.sprite = newItem.Data.image;
        count = newItem.CurrentStackCount;
        RefreshCount();
    }

    public void InitialiseItemData(InventoryItemData newItem)
    {
        ItemData = newItem;
        image.sprite = newItem.Data.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        if(infoPanel != null)
        {
            infoPanel.ClearInfo();
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(" dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("Left click " + ItemData.Data.name + " Description: " + ItemData.Data.Description);
            infoPanel = FindObjectOfType<InfoPanel>();
            if (infoPanel != null)
            {
                infoPanel.SetInfo(ItemData.Data.name, ItemData.Data.Description, ItemData.Data.image);
            }
            // Call to select the slot in InventoryManager
            inventoryManager.SelectSlotBasedOnItem(this);
        }
    }



}
