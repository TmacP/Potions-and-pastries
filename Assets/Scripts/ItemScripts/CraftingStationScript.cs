using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

public class CraftingStationScript : MonoBehaviour, IInteractable
{

    public CraftingStationData Data;

    public List<InventoryItemData> CurrentItems = new List<InventoryItemData>();
    public List<RecipeData> CurrentValidRecipes = new List<RecipeData>(); //this is based off of what is in Current Items
    public List<RecipeData> PossibleValidRecipes = new List<RecipeData>(); //this is based off of the player inventory
    public List<InventoryItemData> OutgoingItems = new List<InventoryItemData>();
    private bool IsCrafting = false;
    public float CraftingProgress = 0.0f;
    public AssetReference CraftingStationUI;
    public event Action OnRefreshedRecipe;


    //************ IINteractable
    public string InteractionPrompt => GetInteractionPrompt();

    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {
        //Open Crafting UI screen

        if(IsCrafting && CraftingProgress >= 1.0f)
        {
            GameEventManager.instance.GivePlayerItems(OutgoingItems);
            OutgoingItems.Clear();
            IsCrafting = false;
            CraftingProgress = 0.0f;
            return true;
        }
        else if(IsCrafting && CraftingProgress < 1.0f)
        {
            return false;
        }
        else
        {
            if (CraftingStationUI != null)
            {
                GameObject GO = CraftingStationUI.InstantiateAsync().WaitForCompletion();
                if (GO != null)
                {
                    InventoryManager[] Managers = GO.GetComponentsInChildren<InventoryManager>();

                    foreach (InventoryManager manager in Managers)
                    {
                        CraftingInventoryManager craftingInventoryManager = manager as CraftingInventoryManager;
                        if (craftingInventoryManager != null)
                        {
                            craftingInventoryManager.InitializeCraftingInventory(CurrentItems, this);
                        }
                        else
                        {
                            manager.InitializeInventoryManager(GameManager.Instance.PlayerState.Inventory);
                        }
                    }
                    RecalculateValidRecipes();
                    return true;
                }
            }
        }
        return false;
    }

//********* End of IInteractable

    private void Start()
    {
        RecalculateValidRecipes();
    }

    public void OnItemAdd(InventoryItemData Item)
    {
        //CurrentItems.Add(Item);
        RecalculateValidRecipes();
    }

    public string GetInteractionPrompt()
    {
        if(CraftingProgress >= 1.0f && IsCrafting)
        {
            if(OutgoingItems.Count > 0)
            {
                return "Take: " + OutgoingItems[0].Data.Name;
            }
        }
        else if(IsCrafting)
        {
            return "";
        }

        return Data.InteractionPrompt;
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
            Debug.Log("No Player Controller");
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
                        CurrentItems.RemoveAt(i);
                    }
                }

                GameEventManager.instance.GivePlayerItems(OutgoingItems);
                OutgoingItems.Clear();
                IsCrafting = false;
                CraftingProgress = 0.0f;
                GameEventManager.instance.RefreshInventory();
                RecalculateValidRecipes();
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
                        CurrentItems.RemoveAt(i);
                    }
                }

                Invoke("FinishCraft", RecipeToCraft.CreationTime);
                GameEventManager.instance.CloseMenu();
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
        return true;
    }
}


