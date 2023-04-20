using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public bool isDeactivated;
    public bool startDeactivated;
    private Light thisLight;

    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();

        if (startDeactivated == true)
        {
            isDeactivated = true;
            thisLight.enabled = false;
        }
        else
        {
            isDeactivated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleLight()
    {
        if (isDeactivated == true)
        {
            isDeactivated = false;
            thisLight.enabled = true;
        }
        else
        {
            isDeactivated = true;
            thisLight.enabled = false;
        }
    }
}
