using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjective : MonoBehaviour
{
    public int objectiveID;
    public string objectiveName;
    public bool objectiveActive;
    public bool objectiveCompleted;
    public int objectivePartsTotal;
    public int objectiveStep;

    private Text objMessageField;
    private Text objDoneMessageField;

    private InventorySystem iManager;
    public bool involvesItem;
    public InventoryItem itemRequired;
    public bool involvesItemLoss;
    public InventoryItem itemToLose;

    [Header("Events")]
    public GameEvent onObjectiveActivated;
    public GameEvent onObjectiveCompleted;

    private GameObject objManager;

    private void Awake()
    {
        objManager = GameObject.Find("ObjectiveManager");
        objMessageField = objManager.GetComponent<ObjectiveTracker>().objMessageField;
        objDoneMessageField = objManager.GetComponent<ObjectiveTracker>().objDoneMessageField;
        if (objectiveActive)
        {
            StartQuest();
        }

        if (involvesItemLoss || involvesItem)
        {
            iManager = FindObjectOfType<InventorySystem>();
        }
    }

    public void StartQuest()
    {
        Debug.Log("Objective " + objectiveName + " started.");
        objectiveActive = true;
        StartCoroutine(ShowStartMessage());
        objManager.GetComponent<ObjectiveTracker>().AddObjective(this);
        onObjectiveActivated.Raise(this, null);

        if (involvesItem)
        {
            int i = iManager.IdentifySlotFromItemType(itemRequired);
            if (i > -1)
            {
                AddQuestStep();
            }
        }
    }

    // Update is called once per frame
    public void AddQuestStep()
    {
        if (objectiveActive)
        {
            objectiveStep++;
            if (objectiveStep == objectivePartsTotal)
            {
                objectiveCompleted = true;
                StartCoroutine(ShowEndMessage());
                if (involvesItemLoss)
                {
                    Debug.Log("Removing " + itemToLose);
                    int i = iManager.IdentifySlotFromItemType(itemToLose);
                    iManager.RemoveItem(i);
                }
                onObjectiveCompleted.Raise(this, null);
                objManager.GetComponent<ObjectiveTracker>().RemoveObj(objectiveID);
                Debug.Log("Objective " + objectiveName + " complete.");
            }
        }

    }

    IEnumerator ShowStartMessage()
    {
        if (objMessageField.text != null)
        {
            objMessageField.text = "New objective: " + objectiveName;
            yield return new WaitForSeconds(5);
            objMessageField.text = null;
        }
        else
        {
            objMessageField.text = "Multiple new objectives!";
            yield return new WaitForSeconds(5);
            objMessageField.text = null;
        }
    }

    IEnumerator ShowEndMessage()
    {
        if (objDoneMessageField.text != null)
        {
            objDoneMessageField.text = "Objective complete: " + objectiveName;
            yield return new WaitForSeconds(5);
            objDoneMessageField.text = null;
        }
        else
        {
            objDoneMessageField.text = "Multiple objectives completed!";
            yield return new WaitForSeconds(5);
            objDoneMessageField.text = null;
        }

    }

}