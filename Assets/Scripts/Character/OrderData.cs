using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderData
{
    public GameObject NPCTarget;
    public ItemData Item; //for later
    public int OrderId;

    public List<EItemTags> NPCLikes;
    public List<EItemTags> NPCDislikes;

    public OrderData()
    { 
        NPCLikes = new List<EItemTags>();
        NPCDislikes = new List<EItemTags>();
    }
}
