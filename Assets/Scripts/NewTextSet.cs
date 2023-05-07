using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTextSet : MonoBehaviour
{
    public TextPopup updateThis;
    public string[] newPopupTexts;
    public int existingTextFields;

    [Header("Events")]
    public GameEvent onNewTextExhausted;

    // Start is called before the first frame update
    void Start()
    {
        existingTextFields = updateThis.popupTexts.Length;
    }

    public void UpdateTextFields()
    {
        int linesDone = 0;
        foreach (string textField in newPopupTexts)
        {
            Debug.Log("Text field " + linesDone + " updating");
            updateThis.popupTexts[linesDone] = newPopupTexts[linesDone];
            linesDone++;
        }

        if (existingTextFields > newPopupTexts.Length)
        {
            while (linesDone <= existingTextFields - 1)
            {
                updateThis.popupTexts[linesDone] = null;
                linesDone++;
            }
        }

        updateThis.messagePlayed = false;
        updateThis.onTextExhausted = onNewTextExhausted;
        updateThis.isRepeatableMessage = true;
        updateThis.ResetTextField();
        updateThis.isRepeatableMessage = false;

    }
}
