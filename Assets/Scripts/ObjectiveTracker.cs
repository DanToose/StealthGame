using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{

    public List<GameObjective> objectiveList = new List<GameObjective>();
    public int objID;

    // Start is called before the first frame update
    void Start()
    {
        objID = 0;
        foreach (Transform child in transform)
        {
            objectiveList.Add(child.gameObject.GetComponent<GameObjective>());
            child.GetComponent<GameObjective>().objectiveID = objID;
            objID++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
