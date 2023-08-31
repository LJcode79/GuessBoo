using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ljTogglePointer : MonoBehaviour
{
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean pointerToggle;
    GameObject pointerOb;
    GameObject journalOb;
    GameObject flashlight;

    public GameObject[] revealObjectList;
    public AppManager appMan;
    //private bool isOn = true;

    int index = 0;
    // Start is called before the first frame update
    void Start()
    {

        pointerOb = GameObject.Find("PR_Pointer");
        journalOb = GameObject.Find("ghostJournal");
        flashlight = GameObject.Find("flashLight");
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerToggle.GetStateDown(targetSource))
        {
            index += 1;
            if (index == 3)
            {
                index = 0;
            }
        }
        switch (index)
        {
            case 0:
                pointerOb.SetActive(true);
                journalOb.SetActive(true);
                flashlight.SetActive(false);

                //hide flashlight obs
                for (int i = 0; i < revealObjectList.Length; i++)
                {
                    revealObjectList[i].SetActive(false);
                }

                break;
            case 1:
                pointerOb.SetActive(false);
                journalOb.SetActive(false);
                flashlight.SetActive(true);

                //reveal all flashlight obs
                if (appMan.selection == 0)
                {
                    for (int i = 0; i < revealObjectList.Length; i++)
                    {
                        revealObjectList[i].SetActive(true);
                    }
                }

                break;
            case 2:
                pointerOb.SetActive(false);
                journalOb.SetActive(false);
                flashlight.SetActive(false);

                //hide flashlight obs
                for (int i = 0; i < revealObjectList.Length; i++)
                {
                    revealObjectList[i].SetActive(false);
                }

                break;
            default:
                Debug.Log("invalid tool");
                break;
        }
        
    }
}
