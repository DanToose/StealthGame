using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupThing : MonoBehaviour
{
    public bool isCollectableToInv;
    public bool isCarryable;
    public bool isRunOverPickUp;

    private GameObject player;

    [Header("Events")]
    public GameEvent onThingAcquired;

    // NOTE - You MUST have an event assigned here if you want to delete up the object, even if you don't need one, because the code will exit on a null event.

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isRunOverPickUp)
        {
            CollectionEvent();
            Destroy(gameObject);

            if (this.GetComponent<InvItemID>() != null)
            {
                GameObject itemToGrab = this.gameObject;
                player.GetComponent<BasicInteract>().RunoverPickup(itemToGrab); // ADD TO INVENTORY LIST
            }
        }
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }

    public void CollectionEvent()
    {
        if (onThingAcquired != null)
        {
            onThingAcquired.Raise(this, null);
        }

    }
}
