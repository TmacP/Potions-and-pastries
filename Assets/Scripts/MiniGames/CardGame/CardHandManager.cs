using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CardHandManager : Toolbar
{
    DeckManager Deck;
    public int HandSize = 4;
    public int MaxHandSize = 7;

    [SerializeField] private GameObject CardPrefab;

    public void Start()
    {
        Deck = PlayerController.instance.GetComponent<DeckManager>();

        //Clear Inventory
        for (int i = ToolbarManager.inventorySlots.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(ToolbarManager.inventorySlots[i].gameObject);
        }
        ToolbarManager.inventorySlots.Clear();

        DrawToFull();
    }

    public void OnEnable()
    {
        GameEventManager.instance.OnCraftComplete += DrawToFull;//DrawCards;
    }

    public override void OnDisable() 
    {
        base.OnDisable();
        GameEventManager.instance.OnCraftComplete -= DrawToFull;
    }

    public int GetHandSize()
    {
        return ToolbarManager.inventorySlots.Count;
    }

    public void DrawCards(int CardsToDraw)
    {
        for(int i = 0; i < CardsToDraw; i++)
        {
            DrawCard();
        }
    }

    public void DrawToFull(int CardsToDraw = -1)
    {
        //int Max = CardsToDraw > 0? CardsToDraw : HandSize;
        int Count = 0;
        foreach (var slot in ToolbarManager.inventorySlots)
        {
            Assert.IsNotNull(slot.gameObject);
            Count++;
        }
        Debug.Log(Count);
        if(Count < HandSize)
        {
            for (; Count < HandSize; Count++)
            {
                DrawCard();
            }
        }
    }

    public override bool UseSelectedItem(ECardActionType Action)
    {
        InventoryItemData Item = GetSelectedItem();
        int slotIndex = -1;
        if(Item != null)
        {
            slotIndex = Item.InventoryIndex;
            if (Item.CardActionType == ECardActionType.Use_Discard && Action != ECardActionType.Use_Trash)
            {
                Deck.DiscardCard(GetSelectedItem());
            }
        }
        bool Success = base.UseSelectedItem(Action);
        //if (Success && Deck != null)
        //{
        //    DrawCard();
        //}

        if(slotIndex >= 0)
        {
            for(int i = slotIndex; i < ToolbarManager.InventoryDataRef.Count; i++)
            {
                InventoryItemData InvItem = ToolbarManager.InventoryDataRef[i];
                if(InvItem != null && InvItem.InventoryIndex >= slotIndex)
                {
                    InvItem.InventoryIndex--;
                }
            }


            DestroyImmediate(ToolbarManager.inventorySlots[slotIndex].gameObject);
            Assert.IsNull(ToolbarManager.inventorySlots[slotIndex]);
            ToolbarManager.inventorySlots.RemoveAt(slotIndex);
        }

        

        ToolbarManager.RefreshInventory();
        return Success;
    }

    public void DrawCard()
    {
        InventoryItemData Card = Deck.DrawCard();

        
        if (Card != null)
        {
            AddCardToHand(Card);
        }
    }

    public void AddCardToHand(InventoryItemData Card)
    {
        GameObject GO = Instantiate(CardPrefab, ToolbarManager.transform);
        InventorySlot slot = GO.GetComponent<InventorySlot>();
        Assert.IsNotNull(slot);
        ToolbarManager.inventorySlots.Add(slot);
        slot.inventoryManager = ToolbarManager;
        ToolbarManager.AddItem(Card);
    }

    public override bool IsFull()
    {
        return ToolbarManager.inventorySlots.Count >= MaxHandSize;
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
