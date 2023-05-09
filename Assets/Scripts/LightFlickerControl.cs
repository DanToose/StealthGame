using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerControl : MonoBehaviour
{
    private Light thisLight;
    public bool makeItFlicker;
    public bool isFlickering;
    public float timeDelay;
    public float minTimeDelay = 0.01f;
    public float maxTimeDelay =  0.2f;
    private bool intensityNormal;
    public float mainIntensity = 1.0f;
    public float altIntensity = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();
        intensityNormal = true;

        if (maxTimeDelay < minTimeDelay)
        {
            maxTimeDelay = minTimeDelay;
        }
        if (minTimeDelay > maxTimeDelay)
        {
            minTimeDelay = maxTimeDelay;
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        if (!isFlickering && makeItFlicker)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    public void ToggleFlickering()
    {
        makeItFlicker = !makeItFlicker;
    }

    public void ToggleLightState()
    {
        thisLight.enabled = !thisLight.enabled; // NOTE - This doesn't work on a flickering light.
    }

    public void ToggleIntensity()
    {
        intensityNormal = !intensityNormal;
        if (intensityNormal)
        {
            thisLight.intensity = mainIntensity;
        }
        else if (!intensityNormal)
        {
            thisLight.intensity = altIntensity;
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        thisLight.enabled = false;
        timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
        yield return new WaitForSeconds(timeDelay);
        thisLight.enabled = true;
        timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
