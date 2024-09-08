using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEngine : MonoBehaviour
{   
    //Chase

    // getting the pos of our player
    public Transform moveToPos;
    NavMeshAgent playerAgent;

    // Start is called before the first frame update
    void Start()
    {
        //Chase

        playerAgent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        //chase

        ChasePlayer();


    }

    void ChasePlayer()
    {
        //inside the componet we acsses (destination) and make our purser chase it.
        playerAgent.destination = moveToPos.position;
    }
}
