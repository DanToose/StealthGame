using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> Items = new List<InventoryItem>(); // Gens a list of Inventory Items, called Items

    public Canvas invCanvas;
    public Transform g_inventoryPanel;  // Reference to InventoryUI graphic (panel)
    public List<InvSlotUi> g_slots = new List<InvSlotUi>();
    public KeyCode inventoryKey;

    // SETS UP THE INVENTORY UI SLOTS
    void Awake()
    {
        foreach (Transform slotGraphic in g_inventoryPanel)
        {
            g_slots.Add(slotGraphic.GetComponent<InvSlotUi>());
            if (slotGraphic.GetComponent<InvSlotUi>().g_item == null)// New
            {
                slotGraphic.gameObject.SetActive(false);       
            }
        }

        invCanvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(inventoryKey))
        {
            invCanvas.enabled = !invCanvas.enabled;
        }
    }

    // THIS FUNCTION ADDS AN INVENTORY ITEM TO THE INVENTORY LIST IF THERE IS SPACE, THEN UPDATES THE UI
    public void AddItem(InventoryItem item)
    {
        if (Items.Count < g_slots.Count) // checks there are free  slots
            Items.Add(item);

        foreach (InvSlotUi slot in g_slots)
        {
            if (slot.Item == null)
            {
                slot.gameObject.SetActive(true); // Turns the UI slot on
                slot.SetItem(item);
                return;
            }
        }
    }

    // THIS FUNCTION SEARCHES THROUGH THE INVENTORY LOOKING FOR ITEMS OF A TYPE - IT RETURNS AN INDEX OF THE LAST ONE FOUND
    public int IdentifySlotFromItemType(InventoryItem name) 
    {
        int slotIndex = 0;
        int foundAtIndex = -1;
        foreach (InvSlotUi slot in g_slots)
        {
            if (slot.Item == name)
            {
                foundAtIndex = slotIndex;
            }
            slotIndex++;
        }

        return foundAtIndex;
    }

    // THIS FUNCTION REMOVES AN INVENTORY ITEM BASED ON AN INDEX VALUE, THEN UPDATES THE LIST AND UI.
    public void RemoveItem(int itemToDrop)
    {
        Items.Remove(Items[itemToDrop]); // Removes the indexed item from the 'Items' list. NOT the UI.
        g_slots[itemToDrop].ClearItem(); // Clears the UI slot for the removed item.

        for (int i = itemToDrop; i < g_slots.Count - 1; i++) // This goes through the g_slots from the item dropped, and copies the next item 'down'
        {
            g_slots[i].SetItem(g_slots[i + 1].Item);
            if (g_slots[i].Item == null)
            {
                g_slots[i].gameObject.SetActive(false);
            }
        }
    }
}
