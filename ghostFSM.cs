using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ghostFSM : MonoBehaviour
{
    public enum State
    {
        roaming,
        hunting,
        invincible
    }

    public GameObject angerIcon;

    public State ghostState;
    public GameObject player;

    public int pointIndex;
    private float timeElapsed;
    private float huntBegin;
    public bool isHunting;
    public bool calmed;
    public bool isInvincible;
    public GameObject[] roamPointList;
    bool goingToPlayer;

    private float huntTime;
    private int firstDest;
    private int randomDest;
    private int oldRandomDest;
    private int randomPointGenerated;
    private bool isFirstDest;
    private bool randDestReached;
    private bool setDestAlready;
    private bool destSet;
    public UnityEngine.AI.NavMeshAgent agent;

    public ljGhostDetection ghostDetectScript;
    // Start is called before the first frame update
    void Start()
    {
        setDestAlready = false;
        destSet = false;
        goingToPlayer = false;
        oldRandomDest = -999;
        //agent.SetDestination(roamPointList[generateInt(0, roamPointList.Length)].transform.position);
        //pointIndex = 0;
        randDestReached = false;

        firstDest = generateInt(0, roamPointList.Length);
        timeElapsed = 0;
        ghostState = State.roaming;

        float min = 10f;
        float max = 20f;
        huntBegin = Random.Range(min, max);
        huntTime = -1f;

        ghostDetectScript = GetComponent<ljGhostDetection>();
        //roamPointList = new GameObject[4];
        calmed = true;
        isInvincible = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(roamPointList[randomDest]);
        if (isFirstDest)
        {
            agent.SetDestination(roamPointList[firstDest].transform.position);
            isFirstDest = false;
        }
        //Debug.Log("HUNT BEGINS IN: " + (huntBegin - timeElapsed));
       // Debug.Log("ENDS AT: " + huntTime);
        huntTime -= Time.deltaTime;
        timeElapsed += Time.deltaTime;
        switch (ghostState)
        {
            case State.roaming:
                //Debug.Log("Roaming State");
                //agent.SetDestination(roamPointList[generateInt(0, roamPointList.Length)].transform.position);

                if ((timeElapsed >= huntBegin) || (isHunting))
                {
                    //agent.SetDestination(roamPointList[generateInt(0, roamPointList.Length)].transform.position);
                    angerIcon.SetActive(true);
                    huntTime = 120f;
                    calmed = false;
                    isHunting = true;
                    ghostState = State.hunting;
                }
                if (isInvincible)
                {
                    //Debug.Log("LJ12345566787");
                    angerIcon.SetActive(true);
                    isHunting = false;
                    calmed = false;
                    ghostState = State.invincible;
                }
                break;


            case State.hunting:
                //Debug.Log("Hunting State");

                if (ghostDetectScript.canSeePlayer)
                {
                    agent.SetDestination(player.transform.position);
                    setDestAlready = false; 
                }
                else
                {
                    if (!setDestAlready) 
                    { 
                        randomPointGenerated = generateInt(0, roamPointList.Length);
                        setDestAlready = true; 
                        agent.SetDestination(roamPointList[randomPointGenerated].transform.position); 
                    } 
                }
                if ((huntTime <= 0) || (calmed))
                {
                    angerIcon.SetActive(false);
                    agent.SetDestination(roamPointList[generateInt(0, roamPointList.Length)].transform.position);
                    huntBegin = generateFloat(60f, 180f);
                    isHunting = false;
                    calmed = true;
                    ghostState = State.roaming;
                }
                if (isInvincible)
                {
                    isHunting = false;
                    calmed = false;
                    ghostState = State.invincible;
                }
                break;
            case State.invincible:
                agent.SetDestination(player.transform.position);
                //goingToPlayer = true;
                break;
        }  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("roamCheck"))
        {
            while (oldRandomDest == randomDest) //if the point is the same, keep generating.
            {
                randomDest = generateInt(0, roamPointList.Length);
            }
            Debug.Log("randomDest = " + randomDest + "oldDest = " + oldRandomDest);
            if (oldRandomDest != randomDest)
            {
                Debug.Log("WORKS");
                agent.SetDestination(roamPointList[randomDest].transform.position);
                oldRandomDest = randomDest;
            }
            randDestReached = true;
            /*
            //Debug.Log("WORKS" + pointIndex);
            pointIndex += 1;
            if (pointIndex == roamPointList.Length)
            {
                pointIndex = 0;
            }
            */
        }

    }

    float generateFloat(float min, float max)
    {
        float randomFloat = Random.Range(min, max);
        return randomFloat;
    }

    int generateInt(int min, int max)
    {
        int randomInt = Random.Range(min, max);
        return randomInt;
    }
}
