using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu] // makes creating an instance of this via the Create RMB menu
public class InventoryItem : ScriptableObject
{
    [SerializeField] // makes private variables show up in the inspector
    private string g_name = "Enter Item Name...";
    public string Name { get { return g_name; } }

    [SerializeField] // makes private variables show up in the inspector
    private Sprite g_sprite;
    public Sprite Sprite { get { return g_sprite; } }
}
