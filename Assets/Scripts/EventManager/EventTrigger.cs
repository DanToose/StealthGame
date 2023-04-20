using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public MyEvent m_triggerEnterFunction, m_triggerExitFunction;
    
    public void OnTriggerEnter(Collider other)
    {
        m_triggerEnterFunction?.Invoke(0);
    }

    public void OnTriggerExit(Collider other)
    {
        m_triggerExitFunction?.Invoke(0);
    }
}
