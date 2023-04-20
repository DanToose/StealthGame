using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lightSwitchToggle()
    {
        //Debug.Log("followPlayerToggle triggered");
        if (Light1 != null)
        {
            Light1.GetComponent<LightScript>().ToggleLight();
        }
        if(Light2 != null)
        {
            Light2.GetComponent<LightScript>().ToggleLight();
        }
        if (Light3 != null)
        {
            Light3.GetComponent<LightScript>().ToggleLight();
        }
    }
}
