using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName ="CozyData/PlayerStateData")]
public class PlayerStateData : ScriptableObject
{
    public float MoveSpeed;

    [SerializeField] public List<InventoryItemData> Inventory = new List<InventoryItemData>();


    [SerializeField] public List<InventoryItemData> ToolBar = new List<InventoryItemData>();
}


