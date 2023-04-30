using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjective : MonoBehaviour
{
    public int objectiveID;
    public string objectiveName;
    public bool objectiveActive;
    public bool objectiveCompleted;
    public int objectiveStepTotal;
    public int objectiveStep;

    
    // Start is called before the first frame update
    void Start()
    {
        objectiveCompleted = false;

        if (objectiveStepTotal < 1)
        {
            objectiveStepTotal = 1;
        }
    }

    public void StartQuest()
    {
        Debug.Log("Objective " + objectiveName + " started.");
        objectiveActive = true;
    }

    // Update is called once per frame
    public void AddQuestStep()
    {
        objectiveStep++;
        if (objectiveStep == objectiveStepTotal)
        {
            objectiveCompleted = true;
            Debug.Log("Objective " + objectiveName + " complete.");
        }
    }
}
