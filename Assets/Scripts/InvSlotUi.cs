using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlotUi : MonoBehaviour
{
    public Text g_text;
    public Image g_icon;
    public Button g_button;

    public InventoryItem g_item; // local reference to Inventory Item in this script
    public InventoryItem Item { get { return g_item; } set { g_item = value; } }

    private void Awake()
    {
        ClearItem();
    }

    // SET ITEM
    public void SetItem(InventoryItem item)
    {
        this.g_item = item;
        if (this.g_item == null)
        {
            ClearItem();
            return;
        }
        this.g_text.text = item.Name;
        this.g_icon.sprite = item.Sprite;
        this.g_button.interactable = true;
    }

    public void ClearItem()
    {
        Debug.Log("Setting fields to null for " + this.gameObject.name);
        this.g_item = null;
        this.g_text.text = null;
        this.g_icon.sprite = null;
        this.g_button.interactable = false;
    }
}
