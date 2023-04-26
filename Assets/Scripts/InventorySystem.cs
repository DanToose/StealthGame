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

    public void RemoveItem(int itemToDrop)
    {
        Items.Remove(Items[itemToDrop]);
        g_slots[itemToDrop].ClearItem(); //-- This was working at clearing a slot, but not removing it.

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
