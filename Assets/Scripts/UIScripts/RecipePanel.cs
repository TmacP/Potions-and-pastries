using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{

    [SerializeField] private CraftingStationScript CraftingStation;

    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header("Scroll View Events")]
    [SerializeField] private RecipeButtonEvent _eventItemClicked;
    [SerializeField] private RecipeButtonEvent _eventItemOnSelect;
    [SerializeField] private RecipeButtonEvent _eventItemOnSubmit;

    public void InitializeRecipes(CraftingStationScript InCraftingStationScript)
    {
        CraftingStation = InCraftingStationScript;
        
        RefreshItems();
        if (CraftingStation != null)
        {
            CraftingStation.OnRefreshedRecipe += RefreshItems;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void OnDisable()
    {
        CraftingStation.OnRefreshedRecipe -= RefreshItems;
    }

    void RefreshItems()
    {
        foreach(Transform child in _content)
        {
            Destroy(child.gameObject);
        }
        if (CraftingStation != null)
        {
            foreach (var recipe in CraftingStation.Data.CraftableRecipes)
            {
                RecipeButton button = CreateItem(recipe);
                Button buttonUI = button.GetComponent<Button>();
                Assert.IsNotNull(buttonUI);
                if (CraftingStation.PossibleValidRecipes.Contains(recipe))
                {
                    buttonUI.interactable = true;
                }
                else
                {
                    buttonUI.interactable = false;
                }
            }
        }
    }


    private RecipeButton CreateItem(RecipeData recipe)
    {
        GameObject Obj;
        RecipeButton item;

        //create the item
        Obj = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);
        Obj.transform.SetParent(_content.transform);
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);
        Obj.transform.localPosition = new Vector3();
        Obj.transform.localRotation = Quaternion.Euler(new Vector3());
        Obj.name = recipe.name;


        //set the appropraite params
        item = Obj.GetComponent<RecipeButton>();
        item.RecipeNameValue = recipe.name;
        item._recipeData = recipe;
        //item.RecipeDescription = String.Join(", ", requiredItems);
        item.RecipeDescription = "Required: ";
        recipe.RequiredItems.ForEach((itemData) => item.RecipeDescription += itemData.Name + ", ");
        // requiredItems.ForEach((itemData) => item.RecipeImage = /addimagewhenavailble/ );

        //add event listeners
        //item.OnSelectEvent.AddListener((ItemButton) => { HandleEventItemOnSelect(item); });
        //item.OnClickEvent.AddListener((ItemButton) => { HandleEventItemOnClick(item); });
        //item.OnSubmitEvent.AddListener((ItemButton) => { HandleEventItemOnSubmit(item); });
        return item;
    }


    private void HandleEventItemOnSubmit(RecipeButton item)
    {
        _eventItemOnSubmit.Invoke(item);
    }
    private void HandleEventItemOnClick(RecipeButton item)
    {
        _eventItemClicked.Invoke(item);
    }

    private void HandleEventItemOnSelect(RecipeButton item)
    {
        _eventItemOnSelect.Invoke(item);
    }
}
