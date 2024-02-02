using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName ="CozyData/PlayerStateData")]
public class PlayerStateData : ScriptableObject
{
    public float MoveSpeed;

    public List<ItemData> Inventory;
}


