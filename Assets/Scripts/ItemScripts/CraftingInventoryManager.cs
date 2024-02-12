using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingInventoryManager : InventoryManager
{
    [HideInInspector]
    public CraftingStationScript CraftingStation;

    public Image CraftImage;
    public TextMeshProUGUI CraftName;
    public TextMeshProUGUI CraftDescription;

    public void Start()
    {
        OnRefreshedRecipe();
    }

    public void InitializeCraftingInventory(List<InventoryItemData> InventoryRef, CraftingStationScript InCraftingStation)
    {
        CraftingStation = InCraftingStation;
        CraftingStation.OnRefreshedRecipe += OnRefreshedRecipe;
        InitializeInventoryManager(InventoryRef);
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

    public override InventoryItemData RemoveItem(InventoryItemData InvItem)
    {
        InventoryItemData ID = base.RemoveItem(InvItem);
        if(CraftingStation != null)
        {
            CraftingStation.OnItemRemove(InvItem);
        }
        return ID;
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
        CraftingStation.OnRefreshedRecipe -= OnRefreshedRecipe;
    }

}
