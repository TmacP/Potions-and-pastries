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

    public NPCCharacterData(NPCData inData)
    {
        Data = inData;
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