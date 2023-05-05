using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{
    public Collider colliderToEnable; // The collider you want to turn on (or toggle off)

    public void ToggleCollider()
    { 
        colliderToEnable.enabled = !colliderToEnable.enabled; // just changes it from disabled to enabled, or vice versa
    }

}
