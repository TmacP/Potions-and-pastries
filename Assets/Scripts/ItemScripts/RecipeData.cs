using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable, CreateAssetMenu(menuName = "CozyData/RecipeData")]
public class RecipeData : ScriptableObject
{
    List<ItemData> RequiredItems;
    List<ItemData> OutgoingItems;

    float CreationTime;
}
