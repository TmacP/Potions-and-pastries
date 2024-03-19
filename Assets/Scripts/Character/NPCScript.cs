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
    private Rigidbody _Rigidbody;
    public Animator frontAnimator;
    public Animator backAnimator;
    private bool faceBack = false;
    private bool faceLeft = true;



    //*************IInteractable interface***********
    public string InteractionPrompt => GetInteractionPrompt();


    public EInteractionResult TryInteract(InteractorBehavoir InInteractor, List<InventoryItemData> InteractionItem = null)
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
            return EInteractionResult.Success;
       }
       return EInteractionResult.Failure;
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

        _Rigidbody ??= GetComponent<Rigidbody>();
        frontAnimator = transform.Find("F_BaseCharacter").GetComponent<Animator>();
        backAnimator = transform.Find("B_BaseCharacter").GetComponent<Animator>();
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
        if (Vector3.Distance(agent.transform.position, destination) < 0.5 && foundTable)
        {
            WaitSecChangeState(3, ENPCState.WaitForOrder);
        }
        else if (Vector3.Distance(agent.transform.position, destination) < 0.5 && foundDoor)
        {
            Destroy(this.gameObject);
        }
        else if (Vector3.Distance(agent.transform.position, destination) < 10 && pointSet && !foundTable && !foundDoor)
        {
            pointSet = false;
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
                EatOrLeave();
                //WaitSecChangeState(3, ENPCState.Wander); //temp
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
            pointSet = false;
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
            if (Vector3.Distance(agent.transform.position, destination) < 0.5)
            {
                Debug.Log("Made it here");
                foundDoor = false; ///TEMP
                Destroy(this.gameObject);
            }
            else
            {
                agent.SetDestination(destination);
            }
        }

    }

    void EatOrLeave()
    {
        bool ChangeChance = RandomChance(0,200);

        // stay and eat more, but do we move or stay at table?
        if(ChangeChance)
        {
            bool ChangeChance2 = RandomChance(0,200);
            // move from table and rerun from wandering state
            if (ChangeChance2)
            {
                WaitSecChangeState(5, ENPCState.Wander);
            }
            //stay at table and feast more
            // skipping FindTable and going straight to ordering 
            else
            {
                WaitSecChangeState(3, ENPCState.WaitForOrder);
            }
            

        }
        else
        {
            WalkToDoor();
        }
    }




    void WaitSecChangeState(float seconds, ENPCState newStateChange)
    {
        if (this.NextNPCState == ENPCState.None && newStateChange != this.NPCState)
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

    // splits the chance 50/50
    private bool RandomChance(float minNum, float maxNum)
    {
        float RandNum = Random.Range(minNum, maxNum);

        if(RandNum < (maxNum / 2) )
        {
            return true;
        }
        else 
        {
            return false;
        }
        
    }
}
