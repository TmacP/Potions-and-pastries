using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EDialogueConditionSource
{
    None = 0,
    NPCState,
    GameState,
    PlayerState
}

[Serializable]
public enum EDialogueConditionComparison
{
    None = 0,
    Equals,
    LessThan,
    GreaterThan
};

[Serializable]
public enum EDialogueConditionProperty
{
    None = 0,
    Class,
    Favourite,
    Hungry,
    Thirsty,
    ProvidingOrder,
    LastRecivedOrder
}


[Serializable]
public class DialogueCondition
{
    public EDialogueConditionSource Source;
    public EDialogueConditionProperty Property;
    public EDialogueConditionComparison Comparison;
    public string ConditionValue;
}

[Serializable]
public class DialogueData
{
    public int ID;
    public string Dialogue;
    public List<DialogueCondition> Conditions = new List<DialogueCondition>();
};

[Serializable]
public class DialogueDataContainer : ScriptableObject
{
    [SerializeField]
    public List<DialogueData> Data;
}
