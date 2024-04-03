using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class NewRecipePanel_UI : MonoBehaviour
{

    [SerializeField] private CraftingStationData CraftingStation;

    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header("Scroll View Events")]
    [SerializeField] private RecipeButtonEvent _eventItemClicked;
    [SerializeField] private RecipeButtonEvent _eventItemOnSelect;
    [SerializeField] private RecipeButtonEvent _eventItemOnSubmit;


    
    public void Start()
    {
        RefreshItems();
    }

    public void ClosePanel()
    {
        Destroy(this.gameObject);
    }

    void RefreshItems()
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }
        if (CraftingStation != null)
        {
            foreach (var recipe in CraftingStation.CraftableRecipes)
            {
                NewRecipeButton button = CreateItem(recipe);
                Button buttonUI = button.GetComponent<Button>();
                Assert.IsNotNull(buttonUI);
                buttonUI.interactable = true;
            }
        }
    }

    private NewRecipeButton CreateItem(RecipeData recipe)
    {
        GameObject Obj;
        NewRecipeButton item;

        //create the item
        Obj = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);
        Obj.transform.SetParent(_content.transform);
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);
        Obj.transform.localPosition = new Vector3();
        Obj.transform.localRotation = Quaternion.Euler(new Vector3());
        Obj.name = recipe.name;


        //set the appropraite params
        item = Obj.GetComponent<NewRecipeButton>();
        item.SetData(recipe);
        //item.RecipeNameValue = recipe.name;
        //item._recipeData = recipe;
        ////item.RecipeDescription = String.Join(", ", requiredItems);
        //item.RecipeDescription = "Required: ";
        //recipe.RequiredItems.ForEach((itemData) => item.RecipeDescription += itemData.Name + ", ");
        //// requiredItems.ForEach((itemData) => item.RecipeImage = /addimagewhenavailble/ );

        //add event listeners
        //item.OnSelectEvent.AddListener((ItemButton) => { HandleEventItemOnSelect(item); });
        //item.OnClickEvent.AddListener((ItemButton) => { HandleEventItemOnClick(item); });
        //item.OnSubmitEvent.AddListener((ItemButton) => { HandleEventItemOnSubmit(item); });
        return item;
    }
}
