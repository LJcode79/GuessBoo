using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ljChangePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Changing pos");
        transform.position = new Vector3(30f, 30f, 30f);
    }

    public void goToFloor1()
    {
        Debug.Log("FLOOR 1");
        transform.position = new Vector3(-46f, 5.5f, 14f);
    }
    public void goToFloor2()
    {
        transform.position = new Vector3(-46f, 9f, 14f);
    }
    public void goToFloor3()
    {
        transform.position = new Vector3(-46f, 13.2f, 14f);
    }
}
