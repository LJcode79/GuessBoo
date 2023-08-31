using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostFSM1 : MonoBehaviour
{
    public enum State
    {
        idle,
        roaming,
        hunting
    }

    public State ghostState;
    public GameObject player;
    
    private float timeElapsed;
    private float huntBegin;
    public bool isHunting;
    public GameObject[] roamPointList;
    int pointIndex = 0;

    public UnityEngine.AI.NavMeshAgent agent;

    public ljGhostDetection ghostDetectScript;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        ghostState = State.roaming;

        float min = 180f;
        float max = 240f;
        huntBegin = Random.Range(min, max);

        ghostDetectScript = GetComponent<ljGhostDetection>();
        //roamPointList = new GameObject[4];
    }


    // Update is called once per frame
    void Update()
    {

        switch (ghostState)
        {
            case State.roaming:
                Debug.Log("Roaming state");

                pointIndex = 0;
                agent.SetDestination(roamPointList[pointIndex].transform.position);

                if ((timeElapsed >= huntBegin) || (isHunting))
                {
                    ghostState = State.hunting;
                }
                break;
            case State.hunting:
                Debug.Log("Hunting State");

                pointIndex = 0;
                agent.SetDestination(roamPointList[pointIndex].transform.position);
                void OnTriggerEnter(Collider other)
                {
                    if (other.CompareTag("roamCheck"))
                    {
                        pointIndex += 1;
                    }

                }

                if (ghostDetectScript.canSeePlayer)
                {
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    agent.SetDestination(roamPointList[pointIndex].transform.position);
                }

                break;
        }  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("roamCheck"))
        {
            pointIndex += 1;
        }

    }

    float generateFloat(float min, float max)
    {
        float randomFloat = Random.Range(min, max);
        return randomFloat;
    }
}
