using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NPCBehaviour : MonoBehaviour

{
    public enum ENPCState
    {
        None = 0,
        Wander,
        Idle,
        FindTable,
        Order
    }

    public ENPCState NPCState;

    public ENPCState NextNPCState = ENPCState.None;

    [SerializeField] private NPCData Data;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer;

    //for setting points to walk to (Wander or FindTable)
    Vector3 destination;
    bool pointSet;
    [SerializeField] float walkingRange;

    [SerializeField] OrderData NpcOrder;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(UpdateNPCState(NPCState));
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
                Debug.Log("Order state entered");
                GameEventManager.instance.TakeNPCOrder(NpcOrder);

                break;
            default:
                Debug.Log("NPCScript::UpdateNPCState unknown NPC state given");
                break;
        }

        //yield return null;
       // yield return new WaitForSeconds(1);
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
            WaitSecChangeState(3, ENPCState.Wander);
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
        //could take off # for X position depending on how big the table is
        //currently have table part of NavMesh Layer and baked so it is not able to be walked on
        destination = new Vector3(GameObject.FindGameObjectWithTag("Table").transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("Table").transform.position.z);
        //returns true if ground is available for walking
        if (Physics.Raycast(destination, Vector3.down, groundLayer)) 
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
            if (agent.transform.position == destination) WaitSecChangeState(3, ENPCState.Order);
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
