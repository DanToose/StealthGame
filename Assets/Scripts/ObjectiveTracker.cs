using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTracker : MonoBehaviour
{
    public List<GameObjective> Objectives = new List<GameObjective>();
    public Canvas objCanvas;
    public Transform o_objPanel;
    public List<ObjSlotUI> o_slots = new List<ObjSlotUI>();
    public KeyCode objectivesKey;

    public AudioSource sourceToPlay;
    public AudioClip questStartTune;
    public float qStartVolume = 1.0f;
    public AudioClip questEndTune;
    public float qEndVolume = 1.0f;

    public Text objMessageField;
    public Text objDoneMessageField;

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

   
    void Update()
    {
        if (Input.GetKeyUp(objectivesKey))
        {
            objCanvas.enabled = !objCanvas.enabled;
        }
    }

    // ADDS OBJECTIVE TO THE OBJECTIVES LIST, AND UPDATES THE OBJ UI.
    public void AddObjective(GameObjective obj) 
    {
        if (Objectives.Count < o_slots.Count) // checks there are free  slots
        {
            Objectives.Add(obj);
            sourceToPlay.PlayOneShot(questStartTune, qStartVolume);
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

    // USES AN INDEX TO FIND THE OBJECTIVE IN THE LIST AND UI WITH THAT INDEX, THEN REMOVE IT FROM BOTH.
    public void RemoveObj(int objID)
    {
        sourceToPlay.PlayOneShot(questEndTune, qEndVolume); // Plays sound on objective ending!
        int testIndex = 0;
        int slotIndex = 0;
        bool indexFound = false;
        foreach (ObjSlotUI slot in o_slots) // This should find the slot index, then remove that objective 
        {
            if (slot.o_ID == objID && !indexFound)
            {
                slotIndex = testIndex;
                indexFound = true;
            }
            testIndex++;
        }

        Objectives.Remove(Objectives[slotIndex]);
        o_slots[slotIndex].ClearObj();


        for (int i = slotIndex; i < o_slots.Count - 1; i++) // This goes through the o_slots from the Objective removed, and copies the next item 'down' each time
        {
            o_slots[i].SetObj(o_slots[i + 1].Objective);
            if (o_slots[i].Objective == null)
            {
                o_slots[i].gameObject.SetActive(false);
            }
        }
    }

}