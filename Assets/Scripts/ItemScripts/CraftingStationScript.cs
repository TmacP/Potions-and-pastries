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
    public List<RecipeData> CurrentValidRecipes = new List<RecipeData>();
    public List<InventoryItemData> OutgoingItems = new List<InventoryItemData>();
    private bool IsCrafting = false;
    public float CraftingProgress = 0.0f;
    public AssetReference CraftingStationUI;
    public event Action OnRefreshedRecipe;


    //************ IINteractable
    public string InteractionPrompt => GetInteractionPrompt();

    public bool TryInteract(InteractorBehavoir InInteractor)
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
        CurrentValidRecipes.Clear();
        CurrentValidRecipes = Data.CraftableRecipes
            .Where(recipe => recipe.RequiredItems.All(requiredItem => CurrentItems.Any(currentItem => currentItem.Data == requiredItem)))
            .ToList();
        
        OnRefreshedRecipe();
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


            IsCrafting = true;
            CraftingProgress = 0.0f;

            for (int i = 0; i < CurrentItems.Count; i++)
            {
                InventoryItemData InvItem = CurrentItems[i];
                Assert.IsNotNull(InvItem);
                InvItem.CurrentStackCount--; 
                if(InvItem.CurrentStackCount <= 0)
                {
                    CurrentItems.RemoveAt(i);
                }
            }

            Invoke("FinishCraft", RecipeToCraft.CreationTime);
            GameEventManager.instance.CloseMenu();
            return true;
        }
        else
        {
            Debug.Log("Could Not Craft");
            return false;
        }
    }

    public bool FinishCraft()
    {
        Debug.Log("CRAFTING DONE");
        IsCrafting = true;
        CraftingProgress = 1.0f;
        return true;
    }
}


