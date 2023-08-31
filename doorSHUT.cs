using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSHUT : MonoBehaviour
{
    public Transform transform;
    public HingeJoint hinge;
    bool motorOn = true;
    bool closed = false;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(0, 90, 0); //shut door
        //hinge.useMotor = false;
        //hinge.useLimits = false;
        timer = 5.0f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        /*
        if((transform.rotation.y < 10f) && (transform.rotation.y > -10f))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); //shut door
            hinge.useMotor = false;
            hinge.useLimits = false;
            timer = 1.0f;
            if (timer < 0)
            {
                hinge.useMotor = true;
                hinge.useLimits = true;
            }
        }
        **/
        if (timer < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); //shut door
            hinge.useMotor = false;
            hinge.useLimits = false;
        }
    }
}
