using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTextSet : MonoBehaviour
{
    public TextPopup updateThis;
    public string[] newPopupTexts;
    public int existingTextFields;
    public bool newTextIsRepeatable;

    [Header("Events")]
    public GameEvent onNewTextExhausted;

    // Start is called before the first frame update
    void Start()
    {
        existingTextFields = updateThis.popupTexts.Length;
    }

    public void UpdateTextFields()
    {
        existingTextFields = updateThis.popupTexts.Length;

        if (newPopupTexts.Length != existingTextFields)
        {
            updateThis.ResetPopupTexts(newPopupTexts.Length);
        }


        int linesDone = 0;

        foreach (string textField in newPopupTexts)
        {
            Debug.Log("Text field " + linesDone + " updating");
            if (updateThis.popupTexts[linesDone] == null)
            updateThis.popupTexts[linesDone] = newPopupTexts[linesDone];
            linesDone++;
        }

        Debug.Log("exist: " + existingTextFields + " new: " + newPopupTexts.Length);

        updateThis.onTextExhausted = onNewTextExhausted;
        updateThis.isRepeatableMessage = true;
        updateThis.ResetTextField();
        updateThis.isRepeatableMessage = newTextIsRepeatable;
        updateThis.messagePlayed = false;
    }
}
