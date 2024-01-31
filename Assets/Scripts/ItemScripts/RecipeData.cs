using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable, CreateAssetMenu(menuName = "CozyData/RecipeData")]
public class RecipeData : ScriptableObject
{
    public string Description;
    public List<ItemData> RequiredItems;
    public List<ItemData> OutgoingItems;

    public float CreationTime;
    //This gives us the option to tie minigame difficulty to this level later
    public float Level; 
}
