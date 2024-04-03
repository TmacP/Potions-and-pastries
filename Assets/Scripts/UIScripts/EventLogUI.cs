using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct EventLog
{
    public float time;
    public string Log;
}

public class EventLogUI : MonoBehaviour
{
    public List<TextMeshProUGUI> EventSlots = new List<TextMeshProUGUI>();

    public List<EventLog> EventLogs = new List<EventLog>();

    public float DisplayTime = 1.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        foreach(TextMeshProUGUI panel in EventSlots)
        {
            panel.transform.parent.gameObject.SetActive(false);
        }

        GameEventManager.instance.OnGivePlayerItems += OnGivePlayerItems;
        GameEventManager.instance.OnPostPlayerGoldChanged += OnPlayerGoldChanged;
    }


    private void OnDisable()
    {
        GameEventManager.instance.OnGivePlayerItems -= OnGivePlayerItems;
        GameEventManager.instance.OnPostPlayerGoldChanged -= OnPlayerGoldChanged;
    }

    private void Update()
    {
        UpdateLogQueue();
    }


    public void MoveStack()
    {
        for(int i = 0; i < EventSlots.Count; i++)
        {
            if(EventLogs.Count <= i)
            {
                EventSlots[i].transform.parent.gameObject.SetActive(false);
                continue;
            }
            EventSlots[i].transform.parent.gameObject.SetActive(true);
            EventSlots[i].text = EventLogs[i].Log;
        }
    }

    public void AddLogToQueue(string LogText)
    {
        EventLog log = new EventLog();

        log.Log = LogText;
        log.time = Time.time;

        EventLogs.Insert(0, log);
        if(EventLogs.Count > EventSlots.Count)
        {
            EventLogs.RemoveRange(EventSlots.Count, EventLogs.Count - EventSlots.Count);
        }
    }

    public void UpdateLogQueue()
    {
        for(int i = EventLogs.Count - 1; i >= 0; i--)
        {
            if (EventLogs[i].time + DisplayTime <= Time.time) 
            {
                EventLogs.RemoveAt(i);
            }
        }
        MoveStack();
    }

    public void OnGivePlayerItems(List<InventoryItemData> items)
    {
        foreach(InventoryItemData item in items)
        {
            string log = "Gained: " + item.Data.name;
            AddLogToQueue(log);
        }
    }

    public void OnPlayerGoldChanged(long NewGold, long DeltaGold)
    {
        string log = (-DeltaGold).ToString() + "$";
        AddLogToQueue(log);
    }
    
}
