using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateListener : MonoBehaviour
{
    public bool[] conditions;
    private bool testCaseTrue;
    public GameEvent allConditionsTrue;
    
    public void SetConditionTrue(int condNumber)
    {
        conditions[condNumber] = true;
        CheckConditions();
    }

    public void SetConditionFalse(int condNumber)
    {
        conditions[condNumber] = false;
    }

    public void CheckConditions() // Fires an event if all conditions are true.
    {
        testCaseTrue = true;
        for (int i = 0; i < conditions.Length; i++) 
        {
            if (conditions[i] == false)
            {
                testCaseTrue = false;
            }
        }

        if (testCaseTrue)
        {
            allConditionsTrue.Raise(this, null);
        }
    }
}
