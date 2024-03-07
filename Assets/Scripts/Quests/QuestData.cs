using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemRequirement
{
    public int CurrentItemCount;
    public InventoryItemData Item;
}

public class QuestData
{
    public string Name;
    public string Description;
    
    //who gave the quest
    public NPCCharacterData Provider;
    //who do we go to turn in the quest 
    public NPCCharacterData Deliverer;

    //Items required to complete the quest
    public List<ItemRequirement> ItemRequirements;
    
//*************Rewards************
    public List<InventoryItemData> ItemRewards;
    public int GoldRewards;


}


