using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ljGhostDetection : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;
    public float eyeOffset;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public LayerMask doorMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        obstructionMask = obstructionMask | doorMask;
    }

    private IEnumerator FOVRoutine()
    {
        float delay = .2f;
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        //Collider[] rangeChecks = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                
                //LJ TEST 
                Vector3 raycastOrigin = transform.position + new Vector3(0, eyeOffset, 0);
                //LJ TEST END

                //if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) //within the radius and no obstruction
                if (!Physics.Raycast(raycastOrigin, directionToTarget, distanceToTarget, obstructionMask)) //within the radius and no obstruction
                {
                    canSeePlayer = true;
                    Debug.Log("EYES ON!");
                }
                else //within radius and obstruction
                {
                    canSeePlayer = false;
                }
            }
            else //within radius
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}