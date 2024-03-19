using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NewRecipeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _recipeName;
    //[SerializeField] private GameObject ImageContainer;
    [SerializeField] private List<Image> _Images;
    [HideInInspector] public RecipeData _recipeData;


    private void Awake()
    {
        //Image[] images = ImageContainer.GetComponentsInChildren<Image>();
        //foreach (Image image in images)
        //{
        //    _Images.Add(image);
        //}
    }

    public void SetData(RecipeData data)
    {
        _recipeName.text = data.name;

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
    }
    
}
