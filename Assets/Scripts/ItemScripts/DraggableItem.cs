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
    public Text countText;
    private InfoPanel infoPanel;

    [HideInInspector] public ItemData ItemData;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    [HideInInspector] public InventoryManager inventoryManager;

    public void InitialiseItemData(ItemData newItem, InventoryManager manager)
    {
        ItemData = newItem;
        inventoryManager = manager;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void InitialiseItemData(ItemData newItem)
    {
        ItemData = newItem;
        image.sprite = newItem.image;
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
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(" dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left click " + ItemData.name + " Description: " + ItemData.Description);
            infoPanel = FindObjectOfType<InfoPanel>();
            if (infoPanel != null)
            {
                infoPanel.SetInfo(ItemData.name, ItemData.Description, ItemData.image);
            }
            else
            {
                Debug.Log("InfoPanel is not closed or not assigned.");
            }

            // Call to select the slot in InventoryManager
            inventoryManager.SelectSlotBasedOnItem(this);
        }
    }



}
