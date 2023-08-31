/**
 * needed for ghost interacting with door
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform doorTransform;
    public Transform knobTransform;
    public float doorSpeed = 1f;
    public AudioClip openSound;
    public AudioClip closeSound;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool isDoorOpen = false;

    void Start()
    {
        startRotation = doorTransform.rotation;
        endRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);
    }

    void Update()
    {
        if (isDoorOpen)
        {
            doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, endRotation, doorSpeed * Time.deltaTime);
        }
        else
        {
            doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, startRotation, doorSpeed * Time.deltaTime);
        }
    }

    void OnDoorInteract()
    {
        isDoorOpen = !isDoorOpen;
        if (isDoorOpen)
        {
            GetComponent<AudioSource>().PlayOneShot(openSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(closeSound);
        }
    }
}

 */