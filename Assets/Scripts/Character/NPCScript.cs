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
        Order,
        AcceptFood
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



//*************IInteractable interface***********
    public string InteractionPrompt => GetInteractionPrompt();


    public bool TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
    {

       if(NPCState == ENPCState.Order && InteractionItem != null && InteractionItem.Count > 0)
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
        if (NPCState == ENPCState.Order)
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNPCState(NPCState);
        //Debug.Log(NPCState);
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
            case ENPCState.Order:
                //Debug.Log("Order state entered");
                GameEventManager.instance.TakeNPCOrder(NpcOrder);
                break;
            case ENPCState.AcceptFood:
                GameEventManager.instance.DoneNPCOrder(NpcOrder);
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
        if (Vector3.Distance(transform.position, destination) < 10)
        {
            pointSet = false;
        }
    }

    void StartIdle()
    {
        if (pointSet)
        {
            agent.SetDestination(destination);
        }
        if (!pointSet)
        {
            destination = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z);
            agent.SetDestination(destination);
            WaitSecChangeState(3, ENPCState.FindTable);
        }
        if (Vector3.Distance(transform.position, destination) < 10)
        {
            pointSet = false;
        }

    }

               //need to change based on what scene is called
//        if (SceneManager.GetActiveScene().name == "LindseyNPCScene")
//        {
//            Debug.Log(NPCState);
//            UpdateNPCState(ENPCState.Wander);
//        }
//        else if (SceneManager.GetActiveScene().name != "LindseyNPCScene")
//        {
//            UpdateNPCState(ENPCState.FindTable);
//        }
    

    void FindTablePosition()
    {
        //destination = new Vector3(GameObject.FindGameObjectWithTag("Table").transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("Table").transform.position.z);
        
        
        if (GameObject.FindGameObjectWithTag("Table") != null)
        {
            destination = new Vector3(GameObject.FindGameObjectWithTag("Table").transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("Table").transform.position.z);
            
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
        if (!pointSet)
        {
            FindTablePosition();
        }
        if (pointSet)
        {
            agent.SetDestination(destination);
            //change to order state 3 sec after table is reached
            if ((agent.transform.position - destination).magnitude < 0.5) WaitSecChangeState(3, ENPCState.Order);
        }
    }

    void WaitSecChangeState(float seconds, ENPCState newStateChange)
    {
        if(this.NextNPCState == ENPCState.None)
        {
            this.NextNPCState = newStateChange;
            Invoke("OnUpdateNPCState", seconds);
        }
    }

    void OnUpdateNPCState()
    {
        UpdateNPCState(NextNPCState);
    }

    
}
