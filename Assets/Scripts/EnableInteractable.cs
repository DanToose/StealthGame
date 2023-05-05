using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInteractable : MonoBehaviour
{
    public Interactable interThis;
    // Start is called before the first frame update
    void Start()
    {
        interThis = GetComponent<Interactable>(); 
    }

    public void EnableIntToggle()
    {
        interThis.enabled = !interThis.enabled;
    }

    
}
