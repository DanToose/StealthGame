using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int plankCount;
    public int fireSpellCount;
    public int healingPackCount;

    public Text plankText;
    public Text fireText;
    public Text healingText;

    // Start is called before the first frame update
    void Start()
    {
        plankCount = 0;
        fireSpellCount = 2;
        healingPackCount = 0;
        plankText = GameObject.Find("PlankCount").GetComponent<Text>();
        healingText = GameObject.Find("HealingCount").GetComponent<Text>();
        fireText = GameObject.Find("FireCount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        plankText.text = "Planks: " + plankCount;
        healingText.text = "Healing: " + healingPackCount;
        fireText.text = "Fire Spells: " + fireSpellCount;
        */
    }

    public void AddPlank()
    {
        plankCount++;
    }

    public void AddSpell()
    {
        fireSpellCount++;
    }

    
    public void LostSpell()
    {
        fireSpellCount--;
    }
    
    public void AddHealing()
    {
        healingPackCount++;
    }
}
