using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    public string initialMessage;
    public string[] popupTexts;
    public KeyCode keyToAdvanceText;
    public int textNumber;
    public bool fieldIsActive; 
    public Text textField;
    public GameObject textFieldObject;
    public bool isRepeatableMessage;
    public bool messagePlayed;
    private bool keyWasPressed;

    [Header("Events")]
    public GameEvent onTextExhausted;

    // Start is called before the first frame update
    void Start()
    {
        textFieldObject = GameObject.Find("PopupText");
        textField = textFieldObject.GetComponent<Text>();
        fieldIsActive = false;
        textField.enabled = false;
        messagePlayed = false;
        textNumber = 0;
    }

    // Update is called once per frame

    private void Update()
    {
        keyWasPressed = false;
        keyWasPressed = Input.GetKeyUp(keyToAdvanceText);

        if (fieldIsActive == true && keyWasPressed) // THIS CODE IS WHAT ADVANCES TO THE NEXT LINE OF TEXT
        {
            if (textNumber >= popupTexts.Length) // IF NO MORE LINES TO COME...
            {
                if (!messagePlayed && onTextExhausted != null)
                {
                    onTextExhausted.Raise(this, null); // CALL AN EVENT IF WE HAVE ONE FOR 'onTextExhausted'...
                }
                messagePlayed = true; // MARK THE MESSAGE AS PLAYED, THEN CALL 'ResetTextField'
                ResetTextField();
            }

            if (textNumber < popupTexts.Length && fieldIsActive) // IF MORE LINES TO COME, GO TO THE NEXT LINE.
            {
                textField.text = popupTexts[textNumber];
                textNumber++;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == false) // IF THE FIELD IS NOT ACTIVE, AND PLAYER ARRIVES
        {
            textField.enabled = true; // ACTIVATE THE TEXT FIELD.
            if (textNumber == 0) // IF NO MESSAGES READ YET...
            {
                textField.text = initialMessage; // SHOW THE INITIAL PROMPT TEXT
                messagePlayed = false; // ENSURE THE MESSSAGE IS NOT DEEMED TO BE PLAYED
            }
            fieldIsActive = true; // PREVENTS THE ABOVE CODE FROM FIRING EVERY FRAME AFTER IT HAS DONE ITS JOB.
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == true) // IF THE PLAYER LEAVES THE ZONE, CALL 'ResetTextField'
        {
            ResetTextField();
        }
    }

    public void ResetTextField()
    {
        if (isRepeatableMessage) // IF RESETTING A REPEATABLE MESSAGE, SET THE TEXT BACK TO THE START
        {
            textNumber = 0;
            textField.text = initialMessage;
        }
        else
        {
            textField.text = (""); // OTHERWISE, MAKE THE TEXT NOTHING
        }

        textField.enabled = false;
        fieldIsActive = false;

    }

    public void ResetPopupTexts(int length) // A FUNCTION USED BY 'NewTextSet' TO RESIZE THE ARRAY OF TEXT LINES 
    {
        Array.Resize<string>(ref popupTexts, length);
        popupTexts = new string[length];
    }

}
