using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDeath : MonoBehaviour
{
    float distanceFromGhost;
    public AppManager manager;
    GameObject ghost;
    float ghostSelectTimer = 10f;
    bool ghostSelected;
    // Start is called before the first frame update
    void Start()
    {
        ghostSelected = false;
        manager = GameObject.Find("ApplicationManager").GetComponent<AppManager>();
        //ghost = manager.selectedGhost;
    }

    // Update is called once per frame
    void Update()
    {
        ghostSelectTimer -= Time.deltaTime;
        //Debug.Log("Ghost Select Timer: " + ghostSelectTimer);
        if ((ghostSelectTimer <= 0) && (ghostSelected = false))
        {
            ghost = manager.selectedGhost;
            //Debug.Log("GHOST IS " + ghost.name);
            bool ghostSelected = true;
        }
        //if(ghostSelected)
    }
}
