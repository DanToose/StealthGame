using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> Items = new List<InventoryItem>(); // Gens a list of Inventory Items, called Items

    // Start is called before the first frame update

    public void AddItem(InventoryItem item)
    {
        Items.Add(item);
    }
}
