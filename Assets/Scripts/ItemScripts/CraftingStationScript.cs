using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class CraftingStationScript : MonoBehaviour, IInteractableExtension
{

    public CraftingStationData Data;

    public List<InventoryItemData> CurrentItems = new List<InventoryItemData>();
    public List<RecipeData> CurrentValidRecipes = new List<RecipeData>(); //this is based off of what is in Current Items
    public List<RecipeData> PossibleValidRecipes = new List<RecipeData>(); //this is based off of the player inventory
    public List<InventoryItemData> OutgoingItems = new List<InventoryItemData>();
    public List<InventoryItemData> TempCurrentItems = new List<InventoryItemData>();

    private bool IsCrafting = false;
    public float CraftingProgress = 0.0f;
    public AssetReference CraftingStationUI;
    public event Action OnRefreshedRecipe;
    public GameObject WorldSpaceCraftingUI;
    [HideInInspector]
    public CraftingInventoryManager CraftingInvManager;


    //************ IINteractable
    public string InteractionPrompt => GetInteractionPrompt();

    public string GetInteractionPrompt()
    {
        if (CraftingProgress >= 1.0f && IsCrafting)
        {
            if (OutgoingItems.Count > 0)
            {
                return "Take: " + OutgoingItems[0].Data.Name;
            }
        }
        else if (IsCrafting)
        {
            return "";
        }

        return Data.InteractionPrompt;
    }

    public string GetSecondaryInteractionPrompt(InventoryItemData InteractionItem = null)
    {
        if (InteractionItem == null)
            return "Use Card";
        string result = "";
        if(InteractionItem.CardActionType == ECardActionType.Use_Trash || InteractionItem.CardActionType == ECardActionType.Use_Discard)
        {
            result = "Add " + InteractionItem.Data.Name;

        }
        else
        {
            result = InteractionItem.CardActionType.ToString();
        }
        return result;
    }

    public string GetThirdInteractionPrompt()
    {
        if(!IsCrafting && CraftingProgress < 1f)
        {
            return "Retrieve Cards";
        }
        return "";
    }

    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {
        //Open Crafting UI screen
        if(IsCrafting && CraftingProgress >= 1.0f)
        {
            //Debug.Log("TryInteract - Crafting Done");

            if(PlayerController.instance.toolbar.IsFull())
            {
                //return EInteractionResult.Failure;
                //Debug.Log("Crafting and full");
                
                IsCrafting = false;
                CraftingProgress = 0.0f;
                

                DeckManager Deck = PlayerController.instance.GetComponent<DeckManager>();
                Assert.IsNotNull(Deck);
                foreach(InventoryItemData item in TempCurrentItems)
                {
                    Deck.DiscardCard(item);
                }
                int CardsToDraw = TempCurrentItems.Count;
                TempCurrentItems.Clear();
                GameEventManager.instance.CraftComplete(CardsToDraw);


                GameEventManager.instance.GivePlayerItems(OutgoingItems);
                GameEventManager.instance.DeckSizeChange();
                OutgoingItems.Clear();
                RecalculateValidRecipes();
                return EInteractionResult.Success;
            }
            else
            {
                //Debug.Log("Crafting finished");

                
                IsCrafting = false;
                CraftingProgress = 0.0f;

                DeckManager Deck = PlayerController.instance.GetComponent<DeckManager>();
                Assert.IsNotNull(Deck);
                foreach (InventoryItemData item in TempCurrentItems)
                {
                    Deck.DiscardCard(item);
                }
                int CardsToDraw = TempCurrentItems.Count;
                TempCurrentItems.Clear();
                GameEventManager.instance.CraftComplete(CardsToDraw);
                GameEventManager.instance.GivePlayerItems(OutgoingItems);
                GameEventManager.instance.DeckSizeChange();
                OutgoingItems.Clear();
                RecalculateValidRecipes();

                return EInteractionResult.Success;
            }

            
        }
        else if(IsCrafting && CraftingProgress < 1.0f)
        {
            return EInteractionResult.Failure;
        }
        else
        {
            if(CurrentValidRecipes.Count > 0)
            {
                TryCraft();
                                // play crafting station sfx
                if (this.Data.Name == "Mixer"){
                    SFX.PlayMixer();
                }
                else if (this.Data.Name == "Oven"){
                    SFX.PlayFireplace();
                }
                else if (this.Data.Name == "Cauldron"){
                    SFX.PlayCauldron();
                }
            }
        }
        
        //else
        //{
        //    if (CraftingStationUI != null)
        //    {
        //        GameObject GO = CraftingStationUI.InstantiateAsync().WaitForCompletion();
        //        if (GO != null)
        //        {
        //            InventoryManager[] Managers = GO.GetComponentsInChildren<InventoryManager>();

        //            foreach (InventoryManager manager in Managers)
        //            {
        //                CraftingInventoryManager craftingInventoryManager = manager as CraftingInventoryManager;
        //                if (craftingInventoryManager != null)
        //                {
        //                    craftingInventoryManager.InitializeCraftingInventory(CurrentItems, this);
        //                }
        //                else
        //                {
        //                    manager.InitializeInventoryManager(GameManager.Instance.PlayerState.Inventory);
        //                }
        //            }
        //            RecalculateValidRecipes();
        //            return true;
        //        }
        //    }
        //}
        return EInteractionResult.Failure;
    }

    public EInteractionResult TrySecondaryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItems = null)
    {
        if (InteractionItems != null && InteractionItems.Count > 0 && InteractionItems[0].bIsCard)
        {
            ECardActionType Action = InteractionItems[0].CardActionType;
            if (IsCrafting)
            {
                if(Data.CardActions.Contains(Action))
                {
                    FinishCraft();
                    return EInteractionResult.Success_ConsumeItem;
                }
                else
                {
                    return EInteractionResult.Failure;
                }
            }
            else if (Action == ECardActionType.Use_Trash || Action == ECardActionType.Use_Discard)
            {
                if (WorldSpaceCraftingUI != null)
                {
                    WorldSpaceCraftingUI.SetActive(true);
                    if (CraftingInvManager == null)
                    {
                        CraftingInvManager = WorldSpaceCraftingUI.GetComponentInChildren<CraftingInventoryManager>();
                    }
                    Assert.IsNotNull(CraftingInvManager);
                    Assert.IsNotNull(InteractionItems[0]);
                    CraftingInvManager.AddItem(InteractionItems[0].CreateCopy());
                    return EInteractionResult.Success_ConsumeItem;
                }
            }
                
        }
        return EInteractionResult.Failure;
    }

    public EInteractionResult TryThirdInteract(InteractorBehavoir InInteractor)
    {
        if(!IsCrafting)
        {
            foreach(InventoryItemData Item in CurrentItems)
            {
                TempCurrentItems.Add(Item);
            }
            CurrentItems.Clear();

            GameEventManager.instance.GivePlayerItems(TempCurrentItems);
            GameEventManager.instance.RefreshInventory();
            TempCurrentItems.Clear();
            return EInteractionResult.Success;
        }
        return EInteractionResult.Failure;
    }

    //********* End of IInteractable

    private void Awake()
    {
        Assert.IsNotNull(WorldSpaceCraftingUI);
        CraftingInvManager = WorldSpaceCraftingUI.GetComponentInChildren<CraftingInventoryManager>();
        Assert.IsNotNull(CraftingInvManager);
        if (CraftingInvManager != null)
        {
            CraftingInvManager.InitializeCraftingInventory(CurrentItems, this);
        }
        Assert.IsNotNull(WorldSpaceCraftingUI);
    }

    private void Start()
    {
        RecalculateValidRecipes();
    }

    public void OnItemAdd(InventoryItemData Item, bool IsFull = false)
    {
        //CurrentItems.Add(Item);
        RecalculateValidRecipes();
        SFX.PlayCard();

        //if (IsFull)
        //{
        //    if (CurrentValidRecipes.Count <= 0)
        //    {
        //        CraftingInvManager?.EmptyInventory();
        //    }
        //}
    }

    
    public void OnItemRemove(InventoryItemData Item)
    {
        //CurrentItems.Remove(Item);
        RecalculateValidRecipes();
    }

    //This can be done in O(1) time but it will take assigning each ingrediant a unique binary id
    //We can worry about changing this if it ever is slow
    public void RecalculateValidRecipes()
    {
        //Current Recipes
        CurrentValidRecipes.Clear();
        CurrentValidRecipes = Data.CraftableRecipes
            .Where(recipe => recipe.RequiredItems.All(requiredItem => CurrentItems.Any(currentItem => currentItem.Data == requiredItem)))
            .ToList();


        //Possible Recipes
        PossibleValidRecipes.Clear();

        //Theres definitely a cleaner way than just adding two lists but its fine for now - the lists are small
        List<InventoryItemData> AllItems = new List<InventoryItemData>();
        AllItems.AddRange(CurrentItems);
        if (PlayerController.instance != null && PlayerController.instance._InventoryManager != null)
        {
            AllItems.AddRange(PlayerController.instance._InventoryManager.InventoryDataRef);
        }
        else
        {
            //Debug.Log("No Player Controller");
        }
        PossibleValidRecipes = Data.CraftableRecipes
        .Where(recipe => recipe.RequiredItems.All(requiredItem => AllItems.Any(currentItem => currentItem.Data == requiredItem)))
        .ToList();
        
        if (OnRefreshedRecipe != null)
        {
            OnRefreshedRecipe();
        }
    }

    public bool TryCraft()
    {
        if(CurrentValidRecipes.Count > 0)
        {
            OutgoingItems.Clear();
            RecipeData RecipeToCraft = CurrentValidRecipes[0];
            foreach(ItemData item in RecipeToCraft.OutgoingItems)
            {
                InventoryItemData InvItem = new InventoryItemData(item, -1, -1);
                InvItem.bIsCard = GameManager.Instance.GetGameState() == EGameState.NightState;
                InvItem.CardActionType = ECardActionType.Use_Trash;
                InvItem.bIsFinalRecipe = Data.bCreatesFinalRecipes;
                OutgoingItems.Add(InvItem);
            }


            HashSet<EItemTags> OutgoingTags = new HashSet<EItemTags>();
            foreach(InventoryItemData item in CurrentItems)
            {
                OutgoingTags.AddRange(item.CurrentItemTags);
            }

            foreach(InventoryItemData item in OutgoingItems)
            {
                item.CurrentItemTags.AddRange(OutgoingTags);
            }


            if(RecipeToCraft.CreationTime == 0)
            {
                for (int i = CurrentItems.Count - 1; i >= 0; i--)
                {
                    InventoryItemData InvItem = CurrentItems[i];
                    Assert.IsNotNull(InvItem);
                    InvItem.CurrentStackCount--;
                    if (InvItem.CurrentStackCount <= 0)
                    {
                        TempCurrentItems.Add(InvItem);
                        CurrentItems.RemoveAt(i);
                    }
                }

                //GameEventManager.instance.GivePlayerItems(OutgoingItems);
                //OutgoingItems.Clear();
                IsCrafting = true;
                CraftingProgress = 1.0f;
                //RecalculateValidRecipes();
                GameEventManager.instance.RefreshInventory();
                //RecalculateValidRecipes();
                return true;
            }
            else
            {
                IsCrafting = true;
                CraftingProgress = 0.0f;

                for (int i = CurrentItems.Count - 1; i >= 0; i--)
                {
                    InventoryItemData InvItem = CurrentItems[i];
                    Assert.IsNotNull(InvItem);
                    InvItem.CurrentStackCount--;
                    if (InvItem.CurrentStackCount <= 0)
                    {
                        TempCurrentItems.Add(InvItem);
                        CurrentItems.RemoveAt(i);
                    }
                }

                Invoke("FinishCraft", RecipeToCraft.CreationTime);
                GameEventManager.instance.RefreshInventory();
                //GameEventManager.instance.CloseMenu();
                return true;
            }

            
        }
        else
        {
            Debug.Log("Could Not Craft");
            return false;
        }
    }

    public bool FinishCraft()
    {
        IsCrafting = true;
        CraftingProgress = 1.0f;
        GameEventManager.instance.RefreshInventory();
        return true;
    }

    
}


