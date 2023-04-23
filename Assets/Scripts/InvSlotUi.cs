using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlotUi : MonoBehaviour
{
    public Text slotItemText;
    public Image slotItemImage;
    public Button slotButton;

    private InventoryItem g_item; // local reference to Inventory Item in this script
    public InventoryItem Item { get { return g_item; } set { g_item = value; } }

    // SET ITEM
    public void SetItem(InventoryItem item)
    {
        this.g_item = item;
        this.slotItemText.text = this.g_item.name; //Sets the text field to this item's name
        this.slotItemImage.sprite = item.Sprite;
        this.slotButton.interactable = true;
    }
}
