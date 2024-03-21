using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NPCGenerator : MonoBehaviour
{
    public int NPCCount = 0;
    public GameObject NPCPrefab;
    public List<NPCData> NPCDatas = new List<NPCData>();

    [HideInInspector]
    public List<NPCTracker> NPCS;

    // Start is called before the first frame update
    void Start()
    {

        GameEventManager.instance.OnChangeGameState += OnChangeGameState;

    }

    public void OnDisable()
    {
        GameEventManager.instance.OnChangeGameState -= OnChangeGameState;
    }

    public void OnChangeGameState(EGameState NewState, EGameState OldState)
    {
        if(NewState == EGameState.NightState && OldState == EGameState.MainState) 
        {
            //TrySpawn();
            InvokeRepeating("TrySpawn", 0.5f, 7.5f);
        }
    }

    public void TrySpawn()
    {
        if(NPCS.Count > GameManager.Instance.PersistantGameState.RoomsUnlocked)
        {
            return;
        }

        GameObject GO = Instantiate(NPCPrefab, this.transform.position, Quaternion.identity);

        NPCBehaviour NPC = GO.GetComponent<NPCBehaviour>();
        NPCTracker Tracker = GO.GetComponent<NPCTracker>();
        Assert.IsNotNull(NPC);
        Assert.IsNotNull(Tracker);

        int index = Random.Range(0, NPCDatas.Count);
        NPC.Data = NPCDatas[index];

        NPCS.Add(Tracker);
    }

    public void RemoveNPC(NPCTracker NPC)
    {
        NPCS.Remove(NPC);
    }
}
