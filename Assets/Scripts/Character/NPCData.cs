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
    public string Name;
    public int Friendship;
    public ENPCArchetype Archetype;
    public List<EItemTags> NPCLikes;
    public List<EItemTags> NPCDislikes;
    public Sprite image;
}

[Serializable]
public class NPCCharacterData
{
    public NPCData Data;
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