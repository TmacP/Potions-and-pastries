using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable, CreateAssetMenu(menuName = "CozyData/CraftingStationData")]
public class CraftingStationData : ScriptableObject
{
    public string Name;
    public string InteractionPrompt;
    public string SecondaryInteractionPrompt;

    public List<RecipeData> CraftableRecipes;

    public List<ECardActionType> CardActions;

    public bool bCreatesFinalRecipes = true;

    //Mini Game
    public AssetReference CraftingActionMiniGame;
    public int CraftMiniGameDifficulty;

}
