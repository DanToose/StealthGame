using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    public List<GameObjective> Objectives = new List<GameObjective>();
    public Canvas objCanvas;
    public Transform o_objPanel;
    public List<ObjSlotUI> o_slots = new List<ObjSlotUI>();
    public KeyCode objectivesKey;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform slotGraphic in o_objPanel)
        {
            o_slots.Add(slotGraphic.GetComponent<ObjSlotUI>());
            if (slotGraphic.GetComponent<ObjSlotUI>().o_obj == null)// New
            {
                slotGraphic.gameObject.SetActive(false);
            }
        }

        objCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(objectivesKey))
        {
            objCanvas.enabled = !objCanvas.enabled;
        }
    }

    public void AddObjective(GameObjective obj)
    {
        //objectiveList.Add(obj);

        Debug.Log("AddObjective called " + obj);
        if (Objectives.Count < o_slots.Count) // checks there are free  slots
        {
            Objectives.Add(obj);
            Debug.Log("Objectives.Add fired");
        }

        foreach (ObjSlotUI slot in o_slots)
        {
            if (slot.Objective == null)
            {
                slot.gameObject.SetActive(true); // Turns the UI slot on
                slot.SetObj(obj);
                return;
            }
        }
    }

    public void RemoveObj(int objID)
    {
        int slotIndex = 0;
        bool indexFound = false;
        foreach (ObjSlotUI slot in o_slots)
        {
            if (slot.o_ID == objID && !indexFound)
            {
                Objectives.Remove(Objectives[slotIndex]);
                o_slots[slotIndex].ClearObj(); //-- This was working at clearing a slot, but not removing it.
                indexFound = true;
            }
            slotIndex++;
        }
        


        for (int i = slotIndex; i < o_slots.Count - 1; i++) // This goes through the g_slots from the item dropped, and copies the next item 'down'
        {
            o_slots[i].SetObj(o_slots[i + 1].Objective);
            if (o_slots[i].Objective == null)
            {
                o_slots[i].gameObject.SetActive(false);
            }
        }
    }

}