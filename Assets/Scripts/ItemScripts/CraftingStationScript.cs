using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CraftingStationScript : MonoBehaviour, IInteractable
{

    public CraftingStationData Data;

    public List<InventoryItemData> CurrentItems = new List<InventoryItemData>();
    public List<RecipeData> CurrentValidRecipes = new List<RecipeData>();
    public List<ItemData> OutgoingItems = new List<ItemData>();
    private bool CraftingFinished = false;
    public AssetReference CraftingStationUI;
    public event Action OnRefreshedRecipe;


    //************ IINteractable
    public string InteractionPrompt => Data.InteractionPrompt;

    public bool TryInteract(InteractorBehavoir InInteractor)
    {
        //Open Crafting UI screen

        if(CraftingFinished)
        {
            GameEventManager.instance.GivePlayerItems(OutgoingItems);
            OutgoingItems.Clear();
        }

        if(CraftingStationUI != null)
        {
            GameObject GO = CraftingStationUI.InstantiateAsync().WaitForCompletion();
            if(GO != null)
            {
                InventoryManager[] Managers = GO.GetComponentsInChildren<InventoryManager>();

                foreach(InventoryManager manager in Managers)
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
            }
        }

        return false;
    }

//********* End of IInteractable

    public void OnItemAdd(InventoryItemData Item)
    {
        //CurrentItems.Add(Item);
        RecalculateValidRecipes();
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
            OutgoingItems.AddRange(RecipeToCraft.OutgoingItems);
            CraftingFinished = false;
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
        CraftingFinished =true;
        return true;
    }
}


