using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    //VARS
    public int m_senderID;
    public int m_intensity;
    public Light m_light;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.m_instance.OnChangeLightIntensity.AddListener(ChangeLightIntensity);
    }

    // Change the light Intensity
    private void ChangeLightIntensity(int sender)
    {
        if (m_senderID == sender) //
        {
            m_light.intensity = m_intensity;
        }
    }

}
