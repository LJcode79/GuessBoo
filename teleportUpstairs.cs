using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class teleportUpstairs : MonoBehaviour
{
    NavMeshAgent agent;
    BoxCollider boxColl;
    ghostFSM ghostFSMscript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ghost"))
        {
            //turn off navmesh agent, turn isTrigger off
            agent = other.GetComponent<NavMeshAgent>();
            agent.enabled = false;
            boxColl = other.GetComponent<BoxCollider>();
            boxColl.isTrigger = false;
            //teleport, change point
            other.transform.position = new Vector3(-4.6f, 5.7f, 3.9f);

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
