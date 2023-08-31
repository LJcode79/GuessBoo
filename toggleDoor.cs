using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleDoor : MonoBehaviour
{
    SuburbanHouse.Door doorScript;
    private float closeDoorTimer;
    private bool doorIsOpen;
    //public GameObject triggerObject;
    // Start is called before the first frame update
    void Start()
    {
        doorScript = GetComponent<SuburbanHouse.Door>();
        closeDoorTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        closeDoorTimer -= Time.deltaTime;  
        if((closeDoorTimer <= 0) && (doorIsOpen))
        {
            doorScript.InteractWithThisDoor();
            doorIsOpen = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if((collision.gameObject.name == "HandColliderRight(Clone)") || (collision.gameObject.name == "HandColliderLeft(Clone)"))
        {
            doorScript.InteractWithThisDoor();
            Debug.Log("collided with player");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ghost")
        {
            doorScript.InteractWithThisDoor();
            Debug.Log("triggered open with ghost");
            doorIsOpen = true;
            closeDoorTimer = 10;
        }
    }
}
