using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjSlotUI : MonoBehaviour
{
    public Text o_text;
    //public Text o_parts;
    public Button o_button;
    public int o_ID;
    //string stepsDisplay;

    public GameObjective o_obj; // local reference to Objective in this script
    public GameObjective Objective { get { return o_obj; } set { o_obj = value; } }
    void Awake()
    {
        ClearObj();
    }

    public void SetObj(GameObjective obj)
    {
        this.o_obj = obj;
        if (this.o_obj == null)
        {
            ClearObj();
            return;
        }

        this.o_text.text = obj.objectiveName;
        this.o_ID = obj.objectiveID;
        this.o_button.interactable = true;
    }

    public void ClearObj()
    {
        //Debug.Log("Setting fields to null for " + this.gameObject.name);
        this.o_obj = null;
        //this.o_ID = new int();
        this.o_text.text = null;
        this.o_button.interactable = false;
    }
}
