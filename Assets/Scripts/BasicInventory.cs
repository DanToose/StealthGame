using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicInventory : MonoBehaviour
{

    public List<string> inventoryItems;
    public List<Sprite> inventoryItemPics;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInvItem(string invName, Sprite invPic)
    {
        //itemName = newItem.name.ToString();
        
        inventoryItems.Add(invName);
        inventoryItemPics.Add(invPic);
        //onThingAcquired.Raise(this, itemName);
    }

    public void RemoveInvItem(string rmvItem)
    {
        //inventoryItems.Remove(rmvItem);
    }
}
