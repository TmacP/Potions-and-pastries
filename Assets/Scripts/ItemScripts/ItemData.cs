using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Linq;

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
    //public public AssetReferenceTexture2D Icon;
    public Sprite image;
    public AssetReference SceneAsset;

    public bool stackable = true;
}

[Serializable]
public class InventoryItemData
{
    public ItemData Data;
    public int InventoryIndex;
    public int CurrentStackCount;
    public HashSet<EItemTags> CurrentItemTags = new HashSet<EItemTags>();

    public InventoryItemData(ItemData InData, int InInventoryIndex, int InCurrentStackCount = 1)
    {
        Data = InData;
        CurrentItemTags.AddRange(Data.ItemTags);
        InventoryIndex = InInventoryIndex;
        CurrentStackCount = InCurrentStackCount;
    }


    public static bool IsEquivalent(InventoryItemData lhs, InventoryItemData rhs)
    {
        if(lhs.Data.Name != rhs.Data.Name)
        {
            return false;
        }
        if (!lhs.CurrentItemTags.SequenceEqual(rhs.CurrentItemTags))
        {
            return false;
        }
        return true;

    }
}


