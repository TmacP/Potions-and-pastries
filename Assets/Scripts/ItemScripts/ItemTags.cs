using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum EItemTags
{
//Resource specific tags
    Sweet, 
    Sour,
    Salty, 
    Bitter,
    Fruity,
    Berry,
    Buttery,
    Chocolatey,
    Vanilley,
    Doughy,

};

[Serializable]
public struct ItemTagRelation
{
    public EItemTags Tag;
    public ItemData Data;
}

[Serializable, CreateAssetMenu(menuName = "CozyData/ItemTags")]
public class ItemTags : ScriptableObject
{
    public List<ItemTagRelation> TagRelations = new List<ItemTagRelation>();
}




