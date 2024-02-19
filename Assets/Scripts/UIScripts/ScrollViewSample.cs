using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CraftingScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private GameObject _prefabListItem;

    [Space(10)]
    [Header("Scroll View Events")]
    [SerializeField] private RecipeButtonEvent _eventItemClicked;
    [SerializeField] private RecipeButtonEvent _eventItemOnSelect;
    [SerializeField] private RecipeButtonEvent _eventItemOnSubmit;

    [Space(10)]
    [Header("Default Selected Index")]
    [SerializeField] private int _defaultSelectedIndex = 0;

    [Space(10)]
    [Header("For testing")]
    [SerializeField] private int _testButtonCount = 10;


    // Start is called before the first frame update
    void Start()
    {
        if (_testButtonCount > 0)
        {
            TestCreateItems(_testButtonCount);
        }
    }

    private void TestCreateItems(int count)
    {
        CraftingStationScript craftingStation = FindObjectOfType<CraftingStationScript>();
        if (craftingStation != null)
        {
            foreach (var recipe in craftingStation.Data.CraftableRecipes)
            {
                CreateItem(recipe.name, recipe.RequiredItems);

            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                List<ItemData> fake = new List<ItemData>();
                CreateItem("Item " + i, fake);
            }
        }
        
    }

    private RecipeButton CreateItem(string strName, List<ItemData> requiredItems)
    {
        GameObject Obj;
        RecipeButton item;
        
        //create the item
        Obj = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);
        Obj.transform.SetParent(_content.transform);
        Obj.transform.localScale = new Vector3(1f, 1f, 1f);
        Obj.transform.localPosition = new Vector3();
        Obj.transform.localRotation = Quaternion.Euler(new Vector3());
        Obj.name = strName;

        
        //set the appropraite params
        item = Obj.GetComponent<RecipeButton>();
        item.RecipeNameValue = strName;
        
        //item.RecipeDescription = String.Join(", ", requiredItems);
        item.RecipeDescription = "Required: ";
        requiredItems.ForEach((itemData) => item.RecipeDescription +=  itemData.Name + ", " );  
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