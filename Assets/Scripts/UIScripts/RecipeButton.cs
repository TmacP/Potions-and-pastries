using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerClickHandler
{
    [ SerializeField] private TMP_Text _recipeName;
    [ SerializeField ] private TMP_Text _recipeDescription;
    [SerializeField] private Image _recipeImage;
    [HideInInspector] public RecipeData _recipeData;
    
    [SerializeField]  private RecipeButtonEvent _onSelectEvent;
    [SerializeField]  private RecipeButtonEvent _onSubmitEvent;
    [SerializeField]  private RecipeButtonEvent _onClickEvent;

    public RecipeButtonEvent OnSelectEvent { get => _onSelectEvent; set => _onSelectEvent = value; }
    public RecipeButtonEvent OnSubmitEvent { get => _onSubmitEvent; set => _onSubmitEvent = value; }
    public RecipeButtonEvent OnClickEvent { get => _onClickEvent; set => _onClickEvent = value; }
    public string RecipeNameValue { 
        get => _recipeName.text; 
        set => _recipeName.text = value; 
    }
    public string RecipeDescription { 
        get => _recipeDescription.text; 
        set => _recipeDescription.text = value; }
    public Image RecipeImage { get => _recipeImage; set => _recipeImage = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_onClickEvent != null)
            _onClickEvent.Invoke(this);
        //This isnt an error since there are times where the button is not disabled
        //else
            //Debug.LogError("OnClickEvent is not assigned.");
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (_onSelectEvent != null)
            _onSelectEvent.Invoke(this);
        //This isnt an error since there are times where the button is not disabled
        //else
        //    Debug.LogError("OnSelectEvent is not assigned.");
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (_onSubmitEvent != null)
            _onSubmitEvent.Invoke(this);
        //This isnt an error since there are times where the button is not disabled
        //else
        //    Debug.LogError("OnSubmitEvent is not assigned.");
    }

    public void ObtainSelectionFocus()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        if (_onSelectEvent != null)
            _onSelectEvent.Invoke(this);
        else
            Debug.LogError("OnSelectEvent is not assigned.");
    }


    public void OnButtonClicked()
    {
        GameEventManager.instance.RecipeDataSelected(_recipeData);
    }
}

public class RecipeButtonEvent : UnityEvent<RecipeButton> { }
