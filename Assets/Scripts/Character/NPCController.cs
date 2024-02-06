using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCController : MonoBehaviour
{
    public NavMeshAgent NPC;
    public Camera cameraChoice;
    // Update is called once per frame
    void Update()
    {
        //right mouse
        if (Input.GetMouseButtonDown(1))
        {
            Ray moveposition = cameraChoice.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(moveposition, out var hitInfo))
            {
                NPC.SetDestination(hitInfo.point);
            }
        }
    }
}
