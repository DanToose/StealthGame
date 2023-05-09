using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEventCaller : MonoBehaviour
{

    public GameEvent eventToCall1;
    public GameEvent eventToCall2; 
    public GameEvent eventToCall3;  
    public GameEvent eventToCall4;
    public GameEvent eventToCall5;
    public GameEvent eventToCall6;

    // USE THIS SCRIPT IF YOU HAVE A CASE WHERE YOU NEED TO CALL MORE THAN ONE EVENT AT ONCE.

    public void CallMultipleEvents()
    {
        if (eventToCall1 != null)
        {
            eventToCall1.Raise(this, null);
        }
        if (eventToCall2 != null)
        {
            eventToCall2.Raise(this, null);
        }
        if (eventToCall3 != null)
        {
            eventToCall3.Raise(this, null);
        }
        if (eventToCall4 != null)
        {
            eventToCall4.Raise(this, null);
        }
        if (eventToCall5 != null)
        {
            eventToCall5.Raise(this, null);
        }
        if (eventToCall6 != null)
        {
            eventToCall6.Raise(this, null);
        }

    }

}
