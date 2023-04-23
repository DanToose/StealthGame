using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewZoneCheck : MonoBehaviour
{
    public GameObject parent;
    public Vector3 guardPosition;
    public Transform target;
    private float sightRange;
    private RaycastHit hitThing;
    public bool inLOS = false;
    public Vector3 direction;
    public LayerMask hitLayers;

    public GameObject lightChecker;

    void Start()
    {
        hitLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("Default") | LayerMask.GetMask("Environment");
        target = GameObject.FindGameObjectWithTag("PlayerBody").transform;
        sightRange = parent.GetComponent<NavmeshAgentScript>().sightRange;
        lightChecker = GameObject.Find("lightChecker");

    }

   private void FixedUpdate()
   {
        if (parent.gameObject.GetComponent<NavmeshAgentScript>().AIState < 3)
        {
            RayCastCheck();

            if (inLOS == true)
            {
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 1; // HEAD TOWARDS PLAYER
            }
            else
            {
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 2;
            }            
        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
        {
            //Debug.Log("Player is in enemy view zone");
            RayCastCheck();

            if (inLOS == true)
            {
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 1; // HEAD TOWARDS PLAYER
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody")
        {
            //Debug.Log("Player left enemy view zone");
            inLOS = false;

            if (parent.gameObject.GetComponent<NavmeshAgentScript>().AIState == 1)
            {
                parent.gameObject.GetComponent<NavmeshAgentScript>().AIState = 2;
            }
        }
    }

    private void RayCastCheck()
    {
        guardPosition = parent.transform.position;
        guardPosition.y = 0.417f;

        direction = (target.transform.position - guardPosition).normalized; //direction FROM guard towards player    
        Ray g_ray = new Ray(guardPosition, direction);
        Debug.DrawRay(g_ray.origin, g_ray.direction * sightRange);

        if (Physics.Raycast(guardPosition, direction * sightRange, out hitThing, sightRange, hitLayers))
        {
            if (hitThing.collider.tag != "PlayerBody")
            {
                //Debug.Log("Raycast hit a thing but NOT PlayerBoody. was " + hitThing.collider.tag);
                inLOS = false;
            }
            else
            {
                //Debug.Log("RAYCAST HIT PLAYERBODY - WOOOOOOOOOOOOOOOOOO!");
                if (lightChecker.gameObject.GetComponent<FPSLightCheck>().isVisible == true)
                {
                    inLOS = true;
                }
                else
                {
                    inLOS = false;
                }
                
            }
        }
        else
        {
            //Debug.Log("Raycast hit nothing");
            inLOS = false;
        }
    }
}