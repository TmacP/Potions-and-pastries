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
    string Name;
    string Description;
    EItemType ItemType;
    EItemTags ItemTags;
    AssetReferenceTexture2D Icon;
    AssetReference SceneAsset;
}

