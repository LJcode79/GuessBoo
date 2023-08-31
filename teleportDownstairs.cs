using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportDownstairs : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    BoxCollider boxColl;
    ghostFSM ghostFSMscript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ghost"))
        {
            //turn off navmesh agent, turn isTrigger off
            agent = other.GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.enabled = false;
            boxColl = other.GetComponent<BoxCollider>();
            boxColl.isTrigger = false;
            //teleport, change point
            other.transform.position = new Vector3(-4f, 2f, 4f);

            ghostFSMscript = other.GetComponent<ghostFSM>();

            ghostFSMscript.pointIndex = 1; //first upstairs roam point
            //turn on navmesh agent, turn isTrigger on
            agent.enabled = true;
            boxColl.isTrigger = true;
            agent = null;
            boxColl = null;
        }
    }
}
