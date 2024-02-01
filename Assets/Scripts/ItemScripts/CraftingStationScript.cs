using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingStationScript : MonoBehaviour, IInteractable
{

    public CraftingStationData Data;

    public List<ItemData> CurrentItems = new List<ItemData>();
    public List<RecipeData> CurrentValidRecipes = new List<RecipeData>();
    public List<ItemData> OutgoingItems = new List<ItemData>();

//************ IINteractable
    public string InteractionPrompt => Data.InteractionPrompt;

    public bool TryInteract(InteractorBehavoir InInteractor)
    {
        //Open Crafting UI screen
        return false;
    }

//********* End of IInteractable

    public void OnItemAdd(ItemData Item)
    {
        CurrentItems.Add(Item);
        RecalculateValidRecipes();
    }

    public void OnItemRemove(ItemData Item)
    {
        CurrentItems.Remove(Item);
        RecalculateValidRecipes();
    }

    //This can be done in O(1) time but it will take assigning each ingrediant a unique binary id
    //We can worry about changing this if it ever is slow
    public void RecalculateValidRecipes()
    {
        CurrentValidRecipes.Clear();

        List<RecipeData> FoundRecipes = Data.CraftableRecipes
            .Where(Recipe => Recipe.RequiredItems.All(CurrentItems.Contains))
            .ToList();
    }
}


