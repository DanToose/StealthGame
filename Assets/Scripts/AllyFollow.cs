using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyFollow : MonoBehaviour
{
    public bool followingPlayer;
    public Transform target;
    private Transform selfLocation;
    NavMeshAgent agent;
    public float followProximity;


    // Start is called before the first frame update
    void Start()
    {
        followingPlayer = false;
        agent = GetComponent<NavMeshAgent>();
        followProximity = 3.0f;
        Invoke("FindPlayer", 0.1f);
    }

    void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void followPlayerToggle()
    {
        //Debug.Log("followPlayerToggle triggered");
        if (followingPlayer == false)
        {
            followingPlayer = true;
        }
        else
        {
            followingPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Vector3.Distance(target.transform.position, gameObject.transform.position); //distance from ally to player
        selfLocation = gameObject.transform;
        if (followingPlayer == true)
        {
            if (diff > followProximity)
            {
                //Debug.Log("Ally destination set");
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(selfLocation.position);
            }
        }
    }

}
