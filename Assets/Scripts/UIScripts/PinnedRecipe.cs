using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinnedRecipe : MonoBehaviour
{

    public GameObject RecipeHUD;
    public List<NewRecipeButton> RecipeList;
    public float RecipeHUDScale = 2f;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(GameEventManager.instance != null)
        {
            GameEventManager.instance.OnUpdatePostedRecipesUI += OnUpdateRecipes;
            OnUpdateRecipes();
        }
    }

    private void Start()
    {
        GameEventManager.instance.OnUpdatePostedRecipesUI += OnUpdateRecipes;
        OnUpdateRecipes();
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnUpdatePostedRecipesUI -= OnUpdateRecipes;
    }

    public void OnUpdateRecipes()
    {
        for(int i = RecipeList.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(RecipeList[i].gameObject);
        }
        RecipeList.Clear();
        foreach(RecipeData data in GameManager.Instance.PersistantGameState.PinnedRecipes)
        {
            GameObject GO = Instantiate(RecipeHUD, this.transform);
            GO.transform.localScale *= RecipeHUDScale;
            NewRecipeButton newRecipeHUD = GO.GetComponent<NewRecipeButton>();
            RecipeList.Add(newRecipeHUD);
            newRecipeHUD.SetData(data);
            newRecipeHUD.SetIsPinnned();
        }
    }
}
