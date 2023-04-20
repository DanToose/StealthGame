using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupThing : MonoBehaviour
{
    public bool isCollectableToInv;
    public bool isCarryable;
    public bool isRunOverPickUp;
    
    [Header("Events")]
    public GameEvent onThingAcquired;

    private void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isRunOverPickUp)
        {
            onThingAcquired.Raise(this, null);
            Destroy(gameObject);
        }
    }
}
