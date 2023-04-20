using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class AllyInProximity : MonoBehaviour
{
    public bool nearAlly;
    
    // Start is called before the first frame update
    void Start()
    {
        nearAlly = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player in range of ally");
            nearAlly = true;
            collision.gameObject.GetComponent<FPSInteraction>().nearAlly = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player leaves range of ally");
            nearAlly = false;
            collision.gameObject.GetComponent<FPSInteraction>().nearAlly = false;
        }
    }
}
