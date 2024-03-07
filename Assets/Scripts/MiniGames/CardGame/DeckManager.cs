using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ECardActionType
{
    None = 0,
    GiveItem,
    UseItem,
    Stir,
    Flip
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
        if(Deck.Count <= 0) 
            return null;
        InventoryItemData returnCard = Deck[0];
        Deck.RemoveAt(0);
        return returnCard;
    }
}


