using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public enum ECardActionType
{
    None = 0,
    Use_Trash,
    Use_Discard,
    Stir,
    Mix
}

public class DeckManager : MonoBehaviour
{
    public List<InventoryItemData> Deck;
    public List<InventoryItemData> Discard;
    //public InventoryManager DeckInventoryManager;
    //public InventoryManager DiscardInventoryManager;



    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Deck = GameManager.Instance.PlayerState.Deck;
        Discard = GameManager.Instance.PlayerState.Discard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeGameScene(EGameState NewState)
    {
        CleanDeck();

        if (NewState == EGameState.MainState)
        {
            RecombineDeck();
        }
        else if(NewState == EGameState.NightState)
        {
            FlattenDeckInventory();
        }
    }

    public void CleanDeck()
    {
        List<InventoryItemData> Hand = GameManager.Instance.PlayerState.CardHand;
        Deck.AddRange(Hand);
        Deck.AddRange(Discard);
        Discard.Clear();
        Hand.Clear();
        GameEventManager.instance.DeckSizeChange();

    }


    //Bad but fast shuffle
    public void ShuffleDeck()
    {
        int n = Deck.Count;
        while (n > 1) 
        {
            int index = Random.Range(0, n-1);
            InventoryItemData TempItem = Deck[index];
            Deck[index] = Deck[n];
            Deck[n] = TempItem;
            n--;
        }
    }

    public InventoryItemData DrawCard()
    {
        int prelength = Deck.Count;
        if(Deck.Count <= 0) 
            DiscardToDeck();
        if (Deck.Count <= 0)
            return null;
            InventoryItemData returnCard = Deck[0];
        Deck.RemoveAt(0);
        GameEventManager.instance.DeckSizeChange();
        return returnCard;
    }

    public void DiscardToDeck()
    {

        Deck.AddRange(Discard);
        foreach(InventoryItemData item in Discard)
        {

        }
        Discard.Clear();
        GameEventManager.instance.DeckSizeChange();
    }

    public void FlattenDeckInventory()
    {
        List<InventoryItemData> NewDeck = new List<InventoryItemData>();
        for(int i = 0; i < Deck.Count; i++)
        {
            InventoryItemData Item = Deck[i];
            for(int j = 0; j < Item.CurrentStackCount; j++)
            {
                InventoryItemData NewCard = new InventoryItemData(Item.Data, -1, 1, true);
                NewCard.CardActionType = Item.CardActionType;
                NewDeck.Add(NewCard);
            }
        }


        GameManager.Instance.PlayerState.Deck.Clear();
        foreach(InventoryItemData Item in NewDeck)
        {
            GameManager.Instance.PlayerState.Deck.Add(Item);
        }
    }

    public void RecombineDeck()
    {
        Dictionary<ItemData, InventoryItemData> NewDeckMap = new Dictionary<ItemData, InventoryItemData>();

        foreach(InventoryItemData Item in Deck)
        {
            InventoryItemData NewItem = null;
            ;
            if(NewDeckMap.TryGetValue(Item.Data, out NewItem) && NewItem != null)
            {
                Assert.IsTrue(Item.CurrentStackCount == 1);
                NewItem.CurrentStackCount += Item.CurrentStackCount;
            }
            else
            {
                InventoryItemData NewCard = new InventoryItemData(Item.Data, Item.InventoryIndex, 1, true);
                NewCard.CardActionType = Item.CardActionType;
                NewDeckMap.Add(NewCard.Data, NewCard);
            }
        }

        GameManager.Instance.PlayerState.Deck.Clear();
        foreach(KeyValuePair<ItemData, InventoryItemData> Pair in NewDeckMap)
        {
            GameManager.Instance.PlayerState.Deck.Add(Pair.Value);
        }
    }

    public void DiscardCard(InventoryItemData Item, bool Clone = false)
    {
        InventoryItemData DiscardItem = Clone ? Item.CreateCopy() : Item;
        Discard.Add(DiscardItem);
        GameEventManager.instance.DeckSizeChange();
    }

    public void AddCardToDiscard(InventoryItemData Item)
    {
        Discard.Add(Item);
        GameEventManager.instance.DeckSizeChange();
    }
}


