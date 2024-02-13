using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderData
{
    public GameObject NPCTarget;
    public ItemData Item; //for later

    public List<EItemTags> NPCLikes;
    public List<EItemTags> NPCDislikes;
}
