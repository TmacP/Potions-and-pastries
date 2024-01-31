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
    public string Name;
    public string Description;
    public EItemType ItemType;
    public List<EItemTags> ItemTags;
    public AssetReferenceTexture2D Icon;
    public AssetReference SceneAsset;
}

