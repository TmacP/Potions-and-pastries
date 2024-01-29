using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System;

[Serializable, CreateAssetMenu(menuName = "CozyData/GatherableData")]
public class GatherableData : ScriptableObject
{
    public List<ItemData> CollectableItems;
    public AssetReference MiniGame;
    public int MiniGameDifficulty;
}

