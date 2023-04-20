using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MyEvent : UnityEvent<int> { }

public class EventManager : MonoBehaviour
{
    public static EventManager m_instance;
    
    // Awake is called when game is played
    void Awake()
    {
        m_instance = this;
    }

    // EVENT LIST

    //-- Change a light's intensity
    public MyEvent OnChangeLightIntensity;
    public void ChangeLightIntensity(int sender)
    {
        OnChangeLightIntensity?.Invoke(sender);
    }
    //-- END OF EVENT TRIGGER DEFINITION
}
