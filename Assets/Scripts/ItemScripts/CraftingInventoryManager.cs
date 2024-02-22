using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CraftingInventoryManager : InventoryManager
{
    [HideInInspector]
    public CraftingStationScript CraftingStation;

    [HideInInspector]
    public List<RecipePanel> RecipePanelRef = new List<RecipePanel>();

    public Image CraftImage;
    public TextMeshProUGUI CraftName;
    public TextMeshProUGUI CraftDescription;

    public void Start()
    {
        GameEventManager.instance.OnRecipeDataSelected += OnRecipeSelected;
    }

    public void InitializeCraftingInventory(List<InventoryItemData> InventoryRef, CraftingStationScript InCraftingStation)
    {
        CraftingStation = InCraftingStation;
        CraftingStation.OnRefreshedRecipe += OnRefreshedRecipe;
        InitializeInventoryManager(InventoryRef);

        RecipePanel[] recipePanel = this.transform.parent.GetComponentsInChildren<RecipePanel>();
        foreach(var recipe in recipePanel)
        {
            recipe.InitializeRecipes(InCraftingStation);
            RecipePanelRef.Add(recipe);
        }
    }

    public override bool AddItemAtIndex(InventoryItemData InvItem, int Index, bool UpdateGameLog = true)
    {
        bool result = base.AddItemAtIndex(InvItem, Index);
        if(CraftingStation != null)
        {
            CraftingStation.OnItemAdd(InvItem);
        }
        return result;
    }

    public override InventoryItemData RemoveItem(InventoryItemData InvItem, bool RemoveEntireStack = false)
    {
        InventoryItemData ID = base.RemoveItem(InvItem, RemoveEntireStack);
        if(CraftingStation != null)
        {
            CraftingStation.OnItemRemove(InvItem);
        }
        return ID;
    }

    public override void RefreshInventory()
    {
        base.RefreshInventory();
        if (CraftingStation != null)
        {
            CraftingStation.RecalculateValidRecipes();
        }
    }

    protected override void CloseInventory()
    {
        Destroy(this.gameObject);
        Destroy(this.transform.parent.gameObject);
    }

    public void OnRefreshedRecipe()
    {
        if(CraftingStation.CurrentValidRecipes.Count > 0)
        {
            RecipeData Data = CraftingStation.CurrentValidRecipes[0];

            if(Data.OutgoingItems.Count > 0)
            {
                CraftImage.sprite = Data.OutgoingItems[0]?.image;
            }

            if(CraftName != null)
            {
                CraftName.SetText(Data.name);
            }

            if (CraftDescription != null)
            {
                CraftDescription.SetText(Data.Description);
            }

            CraftImage.gameObject.SetActive(true);
        }
        else
        {
            CraftImage.gameObject.SetActive(false);
            CraftName.SetText("");
            CraftDescription.SetText("");
        }
    }

    public void OnStartCraft()
    {
        CraftingStation.TryCraft();
    }

    public void OnDisable()
    {
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        if (infoPanel != null)
        {
            infoPanel.gameObject.SetActive(false);
        }
        ClearInventory();
        GameEventManager.instance.OnCloseMenu -= CloseInventory;
        GameEventManager.instance.OnRecipeDataSelected -= OnRecipeSelected;
        if(CraftingStation != null)
        {
            CraftingStation.OnRefreshedRecipe -= OnRefreshedRecipe;
        }

    }


    public void OnRecipeSelected(RecipeData recipe)
    {
        Debug.Log(recipe.name);

        InventoryManager PlayerInventory = PlayerController.instance._InventoryManager;
        Assert.IsNotNull(PlayerInventory);
        bool CraftingStationCleared = true;

        for(int i = InventoryDataRef.Count-1; i >= 0; i--)
        {
            CraftingStationCleared = PlayerInventory.AddItem(InventoryDataRef[i]);
            if(!CraftingStationCleared)
            {
                Debug.LogError("Failed to add item: " + InventoryDataRef[i] + " to Player Inventory");
                break;
            }
        }
        InventoryDataRef.Clear();

        foreach(ItemData item in recipe.RequiredItems)
        {
            InventoryItemData InvItem = PlayerInventory.GetItemByType(item);
            if (InvItem == null)
            {
                Debug.LogError("Failed to find item: " + item + " in Player Inventory");
                return;
            }

            AddItem(InvItem);
            PlayerInventory.RemoveItem(InvItem, true);
        }

        GameEventManager.instance.RefreshInventory();
    }
}
