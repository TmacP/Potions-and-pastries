using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


public class NewRecipeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _recipeName;
    //[SerializeField] private GameObject ImageContainer;
    [SerializeField] private List<Image> _Images;
    [HideInInspector] public RecipeData _recipeData;
    [SerializeField] public GameObject _ShowIsPinnedObject;

    private void Awake()
    {
        //Image[] images = ImageContainer.GetComponentsInChildren<Image>();
        //foreach (Image image in images)
        //{
        //    _Images.Add(image);
        //}
    }

    public void Start()
    {
        GameEventManager.instance.OnUpdatePostedRecipesUI += SetIsPinnned;
    }

    public void OnDisable()
    {
        GameEventManager.instance.OnUpdatePostedRecipesUI -= SetIsPinnned;
    }

    public void SetData(RecipeData data)
    {
        _recipeName.text = data.name;
        _recipeData = data;
        for(int i = 0; i < _Images.Count; i++)
        {
            if(i < data.RequiredItems.Count)
            {
                _Images[i].sprite = data.RequiredItems[i].image;
                _Images[i].gameObject.SetActive(true);
            }
            else
            {
                _Images[i].sprite = null;
                _Images[i].gameObject.SetActive(false);
            }
        }
        SetIsPinnned();
    }


    public void OnButtonClicked()
    {
        GameEventManager.instance.PinRecipe(_recipeData);
    }

    public void SetIsPinnned()
    {
        if(GameManager.Instance.PersistantGameState.PinnedRecipes.Contains(_recipeData))
        {
            _ShowIsPinnedObject.SetActive(true);
        }
        else
        {
            _ShowIsPinnedObject.SetActive(false);
        }
    }

}
