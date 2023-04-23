using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.UI;

public class PickupThing : MonoBehaviour
{
    public bool isCollectableToInv;
    public bool isCarryable;
    public bool isRunOverPickUp;

    public string inventoryItemName; // To remove
    public Sprite invItemPicture; // To remove

    
    [Header("Events")]
    public GameEvent onThingAcquired;


    // NOTE - You MUST have an event assigned here if you want to delete up the object, even if you don't need one, because the code will exit on a null event.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isRunOverPickUp)
        {
            //onThingAcquired.Raise(this, null);
            onThingAcquired.Raise(this, inventoryItemName);
            Destroy(gameObject);
        }
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }
}
