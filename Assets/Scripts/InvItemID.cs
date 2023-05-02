using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvItemID : MonoBehaviour
{
    public InventoryItem g_item;
    [SerializeField]
    private int item_id;

    private GameObject player;
    private GameObject invManager;

    public int ID { get { return item_id; } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.gameObject.GetComponent<BasicInteract>().onInvItemTaken.AddListener(PickUpItem); 
        invManager = GameObject.Find("InventoryManager");

        // Event explained - onObjectClicked function from the raycast is the PUBLISHER
        // Then a LISTENER is created to call PickUpItem method.
    }

    private void PickUpItem(int idToCheck)
    {
        if (item_id == idToCheck)
        {
            invManager.GetComponent<InventorySystem>().AddItem(g_item);
        }

    }
}
