using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public GameObject ghostGuessPage;
    public GameObject ghostWinPage;
    public GameObject ghostLosePage;
    public GameObject deadPage;

    public GameObject[] ghostList;
    public GameObject[] ghostItems1;
    public GameObject[] ghostItems2;
    public GameObject[] ghostItems3;
    public GameObject[] roomList;

    public GameObject frontDoor;

    public int selection;
    public GameObject selectedGhost;
    public GameObject player;

    private float distanceFromGhost;

    public GameObject grandmaScare;
    public GameObject boyScare;
    public GameObject soldierScare;

    private float resetTimer;
    private bool needsReset;

    private bool guessSuccess; //lj change 5/13

    UnityEngine.AI.NavMeshAgent agent;
    BoxCollider boxColl;
    ghostFSM ghostFSMscript;

    void Awake()
    {
        //ghostList = new GameObject[3];
        selection = generateInt(0, ghostList.Length);
        agent = ghostList[selection].GetComponent<UnityEngine.AI.NavMeshAgent>();
        boxColl = ghostList[selection].GetComponent<BoxCollider>();
        ghostFSMscript = ghostList[selection].GetComponent<ghostFSM>();

        //set everything false
        for (int i = 0; i < ghostList.Length; i++)
        {
            ghostList[i].SetActive(false);
        }
        for (int i = 0; i < ghostItems1.Length; i++)
        {
            ghostItems1[i].SetActive(false);
        }
        for (int i = 0; i < ghostItems2.Length; i++)
        {
            ghostItems2[i].SetActive(false);
        }
        for (int i = 0; i < ghostItems3.Length; i++)
        {
            ghostItems3[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        guessSuccess = false; //lj change 5/13
        needsReset = false;
        resetTimer = 0f;

        //set ghost active
        selectedGhost = ghostList[selection];
        ghostList[selection].SetActive(true);
        agent.enabled = true;

        switch (selection)
        {
            case 0:
                for (int i = 0; i < ghostItems1.Length; i++)
                {

                    //set favorite items active
                    ghostItems1[i].SetActive(true);
                }

                //teleport ghost to spawn
                   
                agent.enabled = false;
                boxColl.isTrigger = false;

                //teleport, change point
                ghostList[selection].transform.position = roomList[generateInt(0, roomList.Length)].transform.position;
                    

                //turn on navmesh agent, turn isTrigger on
                agent.enabled = true;
                boxColl.isTrigger = true;
                agent = null;
                boxColl = null;
                break;
            case 1:
                for (int i = 0; i < ghostItems2.Length; i++)
                {
                    //set favorite items active
                    ghostItems2[i].SetActive(true);
                }
                    //teleport ghost to spawn
                   
                agent.enabled = false;
                boxColl.isTrigger = false;

                //teleport, change point
                ghostList[selection].transform.position = roomList[generateInt(0, roomList.Length)].transform.position;
                    

                //turn on navmesh agent, turn isTrigger on
                agent.enabled = true;
                boxColl.isTrigger = true;
                agent = null;
                boxColl = null;
                break;
            case 2:
                for (int i = 0; i < ghostItems3.Length; i++)
                {
                    //set favorite items active
                    ghostItems3[i].SetActive(true);
                }

                //teleport ghost to spawn
                    
                agent.enabled = false;
                boxColl.isTrigger = false;

                //teleport, change point
                ghostList[selection].transform.position = roomList[generateInt(0, roomList.Length)].transform.position;
                   

                //turn on navmesh agent, turn isTrigger on
                agent.enabled = true;
                boxColl.isTrigger = true;
                agent = null;
                boxColl = null;
                break;
            default:
                Debug.Log("INVALID GHOST SELECTION");
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("resetTimer: " + resetTimer);
        //Debug.Log("needsReset: " + needsReset);
        if (needsReset == true)
        {
            resetTimer += Time.deltaTime;
        }
        if (resetTimer >= 5f)
        {
            SceneManager.LoadScene(0);
        }
        distanceFromGhost = Vector3.Distance(selectedGhost.transform.position, player.transform.position);
        //.0
        //Debug.Log("distance from ghost = " + distanceFromGhost);
        //distanceFromGhost = Vector3.Distance(transform.position, ghost.transform.position);
        if ((distanceFromGhost <= 1f) && (guessSuccess == false)) //lj change 5/13
        {
            if(selectedGhost.name == "grandmaWS")
            {
                //ENABLE NEEDS RESET FOR RESET LEVEL AFTER JUMPSCARE
                needsReset = true;
                selectedGhost.SetActive(false);
                grandmaScare.SetActive(true);
                ghostGuessPage.SetActive(false); //lj change 5/13
                deadPage.SetActive(true);//lj change 5/13
                ghostLosePage.SetActive(false);//lj change 5/13
            }
            else if(selectedGhost.name == "soldierWS")
            {
                needsReset = true;
                selectedGhost.SetActive(false);
                soldierScare.SetActive(true);
                ghostGuessPage.SetActive(false);//lj change 5/13
                deadPage.SetActive(true);//lj change 5/13
                ghostLosePage.SetActive(false);//lj change 5/13
            }
            else if (selectedGhost.name == "horrorBoyWS")
            {
                needsReset = true;
                selectedGhost.SetActive(false);
                boyScare.SetActive(true);
                ghostGuessPage.SetActive(false);//lj change 5/13
                deadPage.SetActive(true);//lj change 5/13
                ghostLosePage.SetActive(false);//lj change 5/13
                //play scare noise
                //set reset scene timer
            }
            //Debug.Log("Dead");
        }
        //Debug.Log("Distance from ghost: " + distanceFromGhost);
    }

    int generateInt(int min, int max)
    {
        int randomInt = Random.Range(min, max);
        return randomInt;
    }

    public void checkGhost(string guess)
    {
        // if (guess = ghost) {change canvas to wrong or right.}
        if(selectedGhost.name == guess)
        {
            guessSuccess = true; //lj change 5/13
            ghostGuessPage.SetActive(false);
            ghostWinPage.SetActive(true);
            //ghostLosePage.SetActive(false);

            selectedGhost.SetActive(false);
            Debug.Log("Win");
            //frontDoor.transform.rotation = Quaternion.Euler(frontDoor.transform.rotation.x, 90f, frontDoor.transform.rotation.z);
            Quaternion newRotation = Quaternion.Euler(frontDoor.transform.rotation.eulerAngles.x, 90f, frontDoor.transform.rotation.z);
            frontDoor.transform.rotation = newRotation;
        }
        else
        {
            //needsReset = true;
            ghostGuessPage.SetActive(false);
            //ghostWinPage.SetActive(false);
            ghostLosePage.SetActive(true);
            ghostFSMscript.isInvincible = true;
            Debug.Log("Lose, Ghost invincible");
        }
    }
}
