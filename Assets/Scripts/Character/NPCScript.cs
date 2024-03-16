using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NPCBehaviour : MonoBehaviour, IInteractable

{
    public enum ENPCState
    {
        None = 0,
        Wander,
        Idle,
        FindTable,
        DecideOnOrder,
        WaitForOrder,
        Eating,
        Leaving // foundTable = false;
    }

    public ENPCState NPCState;

    public ENPCState NextNPCState = ENPCState.None;
    public DialogueBehavoir _DialogueBehavoir;

    [SerializeField] private NPCData Data;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;

    //for setting points to walk to (Wander or FindTable)
    Vector3 destination;
    bool pointSet;
    [SerializeField] float walkingRange;

    [SerializeField] OrderData NpcOrder;

    bool foundTable;
    bool foundDoor;


//*************IInteractable interface***********
    public string InteractionPrompt => GetInteractionPrompt();


    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {

       if(NPCState == ENPCState.WaitForOrder && InteractionItem != null && InteractionItem.Count > 0)
       {
            GameEventManager.instance.DoneNPCOrder(NpcOrder);
            GameEventManager.instance.RemovePlayerItems(InteractionItem);
            WaitSecChangeState(0.5f, ENPCState.Wander);
        }
       else if(_DialogueBehavoir != null)
       {
            _DialogueBehavoir.TryDialogue();
            return true;
       }
        return false;
    }

//*******end of IInteractable

    public string GetInteractionPrompt()
    {
        if (NPCState == ENPCState.WaitForOrder)
        {
            return "Give Order";
        }
        else
        {
            return "Speak";
        }
    }

    public void Awake()
    {
        _DialogueBehavoir = GetComponent<DialogueBehavoir>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateNPCState(NPCState);
        foundTable = false;
        foundDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(foundTable);
        //Debug.Log(Vector3.Distance(agent.transform.position, destination));

        //Debug.Log(pointSet);
        if (Vector3.Distance(agent.transform.position, destination) < 10 && pointSet)
        {
            pointSet = false;
            WaitSecChangeState(0, NPCState);
        }
        if (Vector3.Distance(agent.transform.position, destination) < 0.5 && foundTable)
        {
        //    Debug.Log("HIT");
            WaitSecChangeState(0, NPCState);
        }

    }

    private void UpdateNPCState(ENPCState newState)
    {

        if (NPCState != newState)
        {
            //Debug.Log("Changing State");
            NextNPCState = ENPCState.None;
        }

        NPCState = newState;
        
        switch (newState)
        {
            case ENPCState.Wander:
                StartWander();
                break;
            case ENPCState.Idle:
                StartIdle();
                break;
            case ENPCState.FindTable:
                WalkToTable();
                break;
            case ENPCState.DecideOnOrder:
                WaitSecChangeState(3, ENPCState.WaitForOrder);
                break;
            case ENPCState.WaitForOrder:
                //Debug.Log("Order state entered");
                GameEventManager.instance.TakeNPCOrder(NpcOrder);
                break;
            case ENPCState.Eating:
                GameEventManager.instance.DoneNPCOrder(NpcOrder);
                break;
            case ENPCState.Leaving:
                //WalkToDoor();
                WaitSecChangeState(3, ENPCState.Wander); //temp
                break;
            default:
                //Debug.Log("NPCScript::UpdateNPCState unknown NPC state given");
                break;
        }
    }

    void WanderDest()
    {
        float rangeZ = Random.Range(-walkingRange, walkingRange);
        float rangeX = Random.Range(-walkingRange, walkingRange);

        destination = new Vector3(transform.position.x + rangeX, transform.position.y, transform.position.z + rangeZ);
        
        //returns true if ground is available for walking
        if (Physics.Raycast(destination, Vector3.down, groundLayer))
        {
            pointSet = true;
        }
    }
    void StartWander()
    {
        if (!pointSet)
        {
            WanderDest();

        }
        if (pointSet)
        {
            WaitSecChangeState(3, ENPCState.Idle);
            //Debug.Log("Point Set");
            agent.SetDestination(destination);
            
        }
    }

    void StartIdle()
    {
            WaitSecChangeState(3, ENPCState.FindTable);
    }

    

    void FindTablePosition()
    {
        GameObject Table = GameObject.FindGameObjectWithTag("Table");
        if (Table != null)
        {
            destination = Table.transform.position;
            foundTable = true;
        }
        else
        {
            UpdateNPCState(ENPCState.Wander);
        }
        //returns true if ground is available for walking
        if (Physics.Raycast(destination, Vector3.down, groundLayer) && destination != null) 
        {
            pointSet = true;
        }
    }
    void WalkToTable()
    {
        if (!foundTable)
        {
            FindTablePosition();
        }
        if (foundTable)
        {
            //Debug.Log(Vector3.Distance(agent.transform.position, destination) < 0.5);
            //not hitting here for some reason???? Stays stuck at table and this is only getting called twice
            if (Vector3.Distance(agent.transform.position, destination) < 0.5)
            {
                Debug.Log("Made it here");
                foundTable = false; ///TEMP
                WaitSecChangeState(3, ENPCState.WaitForOrder);

            }
            else
            {
                agent.SetDestination(destination);
            }
        }

    }


    void FindDoorPosition()
    {
        GameObject Door = GameObject.FindGameObjectWithTag("Door");
        if (Door!= null)
        {
            destination = Door.transform.position;
            foundDoor = true;
        }
        else
        {
            UpdateNPCState(ENPCState.Wander);
        }
        //returns true if ground is available for walking
        if (Physics.Raycast(destination, Vector3.down, groundLayer) && destination != null)
        {
            pointSet = true;
        }
    }
    void WalkToDoor()
    {
        if (!foundDoor)
        {
            FindDoorPosition();
        }
        if (foundDoor)
        {
            agent.SetDestination(destination);

            if (Vector3.Distance(agent.transform.position, destination) < 20)
            {
                WaitSecChangeState(3, ENPCState.Leaving);
                //         foundTable = false; ///TEMP
            }
        }

    }






    void WaitSecChangeState(float seconds, ENPCState newStateChange)
    {
        if (this.NextNPCState == ENPCState.None)
        {
            this.NextNPCState = newStateChange;
            Invoke("OnUpdateNPCState", seconds);
            //set state here
        }
    }

    void OnUpdateNPCState()
    {
        UpdateNPCState(NextNPCState);
        //update state here
    }

    private ENPCState RandomChance(ENPCState currentState, ENPCState changedToState)
    {
        float RandNum = Random.Range(0, 100);

        if(RandNum <= 50)
        {
            return changedToState;
        }
        else 
        {
            return currentState;
        }
        
    }
}
