using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Instance;
    public List<QuestData> Quests = new List<QuestData>();


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.OnQuestGiven += RecieveQuest;
        GameEventManager.instance.OnGivePlayerItems += OnPlayerItemsChange;
        GameEventManager.instance.OnRemovePlayerItems += OnPlayerItemsChange;
    }

    private void OnDisable()
    {
        GameEventManager.instance.OnQuestGiven -= RecieveQuest;
        GameEventManager.instance.OnGivePlayerItems -= OnPlayerItemsChange;
        GameEventManager.instance.OnRemovePlayerItems -= OnPlayerItemsChange;
    }

    public void OnPlayerItemsChange(List<InventoryItemData> Items)
    {
        UpdateQuestStatus();
    }

    public void RecieveQuest(QuestData Quest)
    {
        Quests.Add(Quest);
        UpdateQuestStatus(); //A bit redundant since we only need to update one quest
    }

    public void UpdateQuestStatus()
    {
        Internal_UpdateQuestsStatus();
        GameEventManager.instance.QuestStatusRefreshed();
    }

    //This is setup where we could consider a couroutine at somepoint
    //This could be way more efficient since we actually know what items change but this is simple for now
    private void Internal_UpdateQuestsStatus()
    {
        var Inv = GameManager.Instance.PlayerState.Inventory;
        Dictionary<ItemData, int> ItemCountCache = new Dictionary<ItemData, int>();

        foreach(QuestData Quest in Quests)
        {
            for(int i = 0; i < Quest.ItemRequirements.Count; i++)
            {
                ItemRequirement requirement = Quest.ItemRequirements[i];
                int Count = 0;
                if (ItemCountCache.TryGetValue(requirement.Item.Data, out Count))
                {
                    requirement.CurrentItemCount = Count;
                    continue;
                }

                List<InventoryItemData> Items = Inv.FindAll(item => item.Data == requirement.Item.Data);
                if (Items.Count > 0)
                {
                    foreach(InventoryItemData Item in Items)
                    {
                        Count += Item.CurrentStackCount;
                    }
                }
                ItemCountCache.Add(requirement.Item.Data, Count);
                requirement.CurrentItemCount = Count;
            }
        }
    }



}
