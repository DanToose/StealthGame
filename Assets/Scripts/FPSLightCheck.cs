using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLightCheck : MonoBehaviour
{
    public bool nearLight; //in range of at least one light
    public bool inLight; //really 'in this specific light'
    public bool isVisible; //really 'in SOME light currently'
    private int lightCount;

    //private GameObject[] Lights;  //array version

    private List<GameObject> lightList = new List<GameObject>();

    private Vector3 myPosition;
    private float diff;
    public float lightRange;
    private RaycastHit hitThing;

    public LayerMask hitLayers;

    public bool ignoresLightCheck;
    public bool useHUDvignette;
    public GameObject HUDvignette;

    // Start is called before the first frame update
    void Start()
    {
        nearLight = false;
        inLight = false;
        isVisible = false;

        lightRange = 15;

        hitLayers = LayerMask.GetMask("Player") | LayerMask.GetMask("Default") | LayerMask.GetMask("Environment");

        if (ignoresLightCheck == true)
        {
            isVisible = true;
        }
    }


    void Update()
    {
        myPosition = transform.position;

        if (nearLight == true && ignoresLightCheck == false)
        {
            AuditLights();
        }

        if (useHUDvignette == true)
        {
            if (isVisible == true)
            {

                HUDvignette.gameObject.SetActive(true);
                    //Debug.Log("Player deemed in light now");
            }
            else
            {
                HUDvignette.gameObject.SetActive(false);
                //Debug.Log("Player deemed in shadow now");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            nearLight = true;
            lightCount++;

            lightList.Add(other.gameObject);

            Debug.Log("Light entered trigger. Light total = " + lightCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lightList.Remove(other.gameObject);
            lightCount--;
            Debug.Log("Light left trigger. Light total = " + lightCount);

            if (lightCount <= 0)
            {
                nearLight = false;
            }
        }
    }
    public void AuditLights()
    {
        isVisible = false; //must reset for fresh audit - Will be set to true if any light in range has LOS to player.
        
        foreach (GameObject l in lightList)
        {
            if (l.gameObject.GetComponent<LightScript>().isDeactivated == false) //check to see if the light is deactivated or not
            //Debug.Log(l.gameObject.GetComponent<LightScript>().isDeactivated);
            {
                float diff = Vector3.Distance(l.transform.position, gameObject.transform.position); //distance from player to light
                Vector3 direction = (l.transform.position - myPosition).normalized; //direction FROM player towards light

                if (diff <= lightRange)  // && (inLight == false))
                {
                    Ray g_ray = new Ray(myPosition, direction);
                    Debug.DrawRay(g_ray.origin, g_ray.direction * 15);
                     if (Physics.Raycast(myPosition, direction * diff, out hitThing, hitLayers))
                     {  
                        string tag = hitThing.collider.tag;
                        string name = hitThing.collider.gameObject.name;
                        //Debug.Log("tag" + tag + "Object =" +name);
                        if (hitThing.collider.tag != "Light")
                        {
                            inLight = false;
                        }
                        else
                        {
                            inLight = true;
                            isVisible = true;
                        }
                     }
                }
                        
            }
        }
        //Debug.Log("isVisible =" + isVisible);
    }
}