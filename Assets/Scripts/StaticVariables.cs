using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Objective
{
    public string key;
    public bool value;
    public string HUDtype;
    public Text HUDtext;
    public Image HUDimage;
}
public class StaticVariables : MonoBehaviour
{
    
    public List<Objective> objectiveSet;

    public static StaticVariables _instance;

    public Dictionary<string, bool> dictionaryObjectivesSet = new Dictionary<string, bool>();

    private void Start()
    {
        _instance = this;

        if (_instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    


        foreach (Objective obj in objectiveSet)
        {
            dictionaryObjectivesSet.Add(obj.key, obj.value);
        }
    }

    // Update is called once per frame
    
    private void UpdateList()
    {
        foreach (KeyValuePair<string, bool> dictionaryObj in _instance.dictionaryObjectivesSet)
        {
            foreach (Objective listObj in _instance.objectiveSet)
            {
                if (listObj.key == dictionaryObj.Key)
                {
                    listObj.value = dictionaryObj.Value;
                }

            }
        }
    }
    
    public void UpdateObjective (string key, bool value)
    {
        _instance.dictionaryObjectivesSet[key] = value;
        UpdateList();
    }
    
    void Update()
    {

    }
}
