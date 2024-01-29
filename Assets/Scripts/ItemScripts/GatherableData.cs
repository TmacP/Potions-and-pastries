using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System;

[Serializable, CreateAssetMenu(menuName = "CozyData/GatherableData")]
public class GatherableData : ScriptableObject
{
    public string InteractionPrompt;
    public string GatherableName;

    //A list of items we can recieve from this node
    public List<ItemData> CollectableItems;

    public int ItemsPerInteraction; 
    public int NumberOfInteractions;

    //MiniGame Data
    public AssetReference MiniGame;
    public int MiniGameDifficulty;
}

