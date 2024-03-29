using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENPCArchetype
{
    Rogue,
    Magician,
    Ranger,
    Fighter
}

public enum ECharacterSpriteAssetSlots
{
    Hat,
    Hair,
    Eye,
    Nose,
    Mouth,
    Torso, 
    Arm,
    Bottom,
    Shoe
}

[Serializable]
public struct fCharacterSpriteAssetData
{
    public ECharacterSpriteAssetSlots slot;
    public string AssetName;
    public Color AssetColour;

    public fCharacterSpriteAssetData(ECharacterSpriteAssetSlots InSlots,  string InAssetName, Color InAssetColour)
    {
        slot = InSlots;
        AssetName = InAssetName;
        AssetColour = InAssetColour;
    }
}

[Serializable, CreateAssetMenu(menuName = "CozyData/NPCData")]
public class NPCData : ScriptableObject
{
    public string DefaultName;
    public int DefaultFriendship;
    public ENPCArchetype DefaultArchetype;
    public List<EItemTags> DefaultNPCLikes;
    public List<EItemTags> DefaultNPCDislikes;

    //public Sprite image;
}

[Serializable]
public class NPCCharacterData
{
    public NPCData Data;
    public string Name;
    public int Friendship = 0;
    public ENPCArchetype Archetype;
    public List<EItemTags> NPCLikes;
    public List<EItemTags> NPCDislikes;

    public List<fCharacterSpriteAssetData> SpriteAssetData;

    public NPCCharacterData(NPCData inData)
    {
        Data = inData;
        NPCLikes = new List<EItemTags>();
        NPCDislikes = new List<EItemTags>();
        SpriteAssetData = new List<fCharacterSpriteAssetData>();

    }
}

[Serializable]
public class NPCDialogueState
{
    public string Class;
    public string[] Favourite;
    public string Hungry;
    public string Thirsty;
    public string ProvidingOrder;
    public string LastRecivedOrder;
}
