using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInventory : MonoBehaviour
{

    public List<string> inventoryItems;

    private string itemName;
    private GameObject newItem;
    private GameObject rmvItem;

    [Header("Events")]
    public GameEvent onThingAcquired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInvItem(GameObject newItem)
    {
        itemName = newItem.name.ToString();
        inventoryItems.Add(itemName);

        //onThingAcquired.Raise(this, itemName);
    }

    public void RemoveInvItem(GameObject rmvItem)
    {
        //inventoryItems.Remove(rmvItem);
    }
}
