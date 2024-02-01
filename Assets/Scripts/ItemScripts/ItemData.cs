using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;

public enum EItemType
{
    Resource,
    Equipment,
    Decoration
}

[Serializable, CreateAssetMenu(menuName = "CozyData/ItemData")]
public class ItemData : ScriptableObject
{
    public public string Name;
    public public string Description;
    public public EItemType ItemType;
    public public List<EItemTags> ItemTags;
    //public public AssetReferenceTexture2D Icon;
    public Sprite image;
    public public AssetReference SceneAsset;

    public bool stackable = true;
}

