using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChaseEngine : MonoBehaviour
{
    //JS

    public RawImage JSImage;
    bool isPlayerCaught;

    //Chase

    bool isReturningToResetPoint;
    private Coroutine chaseDelayCoroutine;      // To keep track of the running coroutine
    public Vector3[] PP = new Vector3[6];


    // getting the pos of our player

    public Transform moveToPos;
    public NavMeshAgent playerAgent;

    void Start()
    {
        //chase

        isReturningToResetPoint = true;

    }

    void Update()
    {
        //chase

        ChasePlayer();

        //JS

        CaughtPlayer();


    }

    void ChasePlayer()
    {
        // If the player is in range, chase the player.

        if (PlayerDetection.isPlayerInRange)
        {
            playerAgent.destination = moveToPos.position;
            // Reset the state when chasing the player to ensure correct behavior when losing sight of the player.
            isReturningToResetPoint = true;

            // Stop any running delay coroutine if the player comes back in range
            if (chaseDelayCoroutine != null)
            {
                //Stops the coro if it wasnt null aka is running a coro
                StopCoroutine(chaseDelayCoroutine);

                //emtpy him just in case when he gets filled again we wont have issues
                chaseDelayCoroutine = null;
            }

        }
        else   
        {
            // Checks if no delay coroutine is currently running.
            if (chaseDelayCoroutine == null)
            {
                chaseDelayCoroutine = StartCoroutine(ChasePlayerAfterDelay());
            }
        }
    }
    void CaughtPlayer()
    {
        if (isPlayerCaught)
        {
            JSImage.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //JS check

        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerCaught = true;
        }
        else
        {
            isPlayerCaught = false;
        }
    }
    IEnumerator ChasePlayerAfterDelay()
    {
        // Wait for a few seconds after the player goes out of range
        yield return new WaitForSeconds(2.5f); 

        // Call the function to handle behavior after the delay
        ChasePlayerAfterOutOfRange();
    }
    void ChasePlayerAfterOutOfRange()
    {
        if (isReturningToResetPoint)
        {
            // Set the destination to the reset point (PP[0]) if returning.
            playerAgent.destination = PP[0];

            // Check if the agent has arrived at the reset point.
            //!playerAgent.pathPending = is the path done cacluting
            //playerAgent.remainingDistance <= playerAgent.stoppingDistance: Checks if he is close to the point

            if (!playerAgent.pathPending && playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                // Agent has reached PP[0], switch to roaming between random points.

                isReturningToResetPoint = false;

                // Choose a random point from PP[1] to PP[5].
                playerAgent.destination = PP[Random.Range(1, 6)];
            }
        }
        else //this says basically - isReturningToResetPoint = false
        {
            // The agent is currently roaming; checking if it has arrived at the current random destination just like before
            if (!playerAgent.pathPending && playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                // Agent has reached its current random target; select a new random point.
                playerAgent.destination = PP[Random.Range(1, 6)];
            }
        }

        // Reset the coroutine variable after completion
        chaseDelayCoroutine = null;
    }

}
