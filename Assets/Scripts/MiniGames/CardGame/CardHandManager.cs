using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandManager : Toolbar
{
    DeckManager Deck;

    public void Start()
    {
        Deck = PlayerController.instance.GetComponent<DeckManager>();
        DrawToFull();
    }

    public void OnEnable()
    {
        GameEventManager.instance.OnCraftComplete += DrawToFull;
    }

    public override void OnDisable() 
    {
        base.OnDisable();
        GameEventManager.instance.OnCraftComplete -= DrawToFull;
    }

    public int GetHandSize()
    {
        return ToolbarManager.inventorySlots.Length;
    }

    public void DrawToFull()
    {
        int Count = 0;
        foreach (var slot in ToolbarManager.inventorySlots)
        {
            if (slot != null && slot.IsEmpty())
            {
                Count++;
            }
        }
        for (int i = 0; i < Count; i++)
        {
            DrawCard();
        }
    }

    public override bool UseSelectedItem(ECardActionType Action)
    {
        InventoryItemData Item = GetSelectedItem();
        if(Item != null && Item.CardActionType == ECardActionType.Use_Discard && Action != ECardActionType.Use_Trash)
        { 
            Deck.DiscardCard(GetSelectedItem());
        }
        bool Success = base.UseSelectedItem(Action);
        //if (Success && Deck != null)
        //{
        //    DrawCard();
        //}
        return Success;
    }

    public void DrawCard()
    {
        InventoryItemData Card = Deck.DrawCard();

        if (Card != null)
        {
            ToolbarManager.AddItem(Card);
        }
    }

    public override void UpdateScrollSlot(float ScrollValue)
    {
        if (ScrollValue > 0.1)
        {
            int NewIndex = ToolbarManager.selectedSlot + 1;
            InventorySlot slot = ToolbarManager.GetSlotByIndex(NewIndex);
            if (slot != null && !slot.IsEmpty())
            {
                ToolbarManager.ChangeSelectedSlot(1);
            }
        }
        else if (ScrollValue < -0.1)
        {
            int NewIndex = ToolbarManager.selectedSlot - 1;
            InventorySlot slot = ToolbarManager.GetSlotByIndex(NewIndex);
            if (slot != null && !slot.IsEmpty())
            {
                ToolbarManager.ChangeSelectedSlot(-1);
            }
        }
    }
}
