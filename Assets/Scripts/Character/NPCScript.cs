using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour

{
    //[SerializeField] private NPCData Data;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;

    //patrolling
    Vector3 destination;
    bool pointSet;
    [SerializeField] float walkingRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        StartWalking();
    }

    void StartWalking()
    {
        if (!pointSet)
        {
            WanderDest();
        }
        if (pointSet)
        {
            agent.SetDestination(destination);
        }
        if(Vector3.Distance(transform.position, destination) < 10)
        {
            pointSet = false;
        }
    }

    void WanderDest()
    {
        float rangeZ = Random.Range(-walkingRange, walkingRange);
        float rangeX = Random.Range(-walkingRange, walkingRange);

        destination = new Vector3(transform.position.x + rangeX, transform.position.y, transform.position.z + rangeZ);

        //returns true if ground is available
        if (Physics.Raycast(destination, Vector3.down, groundLayer))
        {
            pointSet = true;
        }
    }
}
