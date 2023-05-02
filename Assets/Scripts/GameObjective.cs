using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjective : MonoBehaviour
{
    public int objectiveID;
    public string objectiveName;
    public bool objectiveActive;
    public bool objectiveCompleted;
    public int objectivePartsTotal;
    public int objectiveStep;

    [Header("Events")]
    public GameEvent onObjectiveActivated;
    public GameEvent onObjectiveCompleted;

    private GameObject objManager;

    private void Awake()
    {
        objManager = GameObject.Find("ObjectiveManager");
        if (objectiveActive)
        {
            StartQuest();
        }
    }

    public void StartQuest()
    {
        Debug.Log("Objective " + objectiveName + " started.");
        objectiveActive = true;
        objManager.GetComponent<ObjectiveTracker>().AddObjective(this);
        onObjectiveActivated.Raise(this, null);
    }

    // Update is called once per frame
    public void AddQuestStep()
    {
        objectiveStep++;
        if (objectiveStep == objectivePartsTotal)
        {
            objectiveCompleted = true;
            objManager.GetComponent<ObjectiveTracker>().RemoveObj(objectiveID);
            Debug.Log("Objective " + objectiveName + " complete.");
        }
    }
}