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

        //textFieldObject.SetActive(false);
        textNumber = 0;
    }

    // Update is called once per frame

    private void Update()
    {
        keyWasPressed = false;
        keyWasPressed = Input.GetKeyUp(keyToAdvanceText);

        if (fieldIsActive == true && keyWasPressed)
        {
            if (textNumber >= popupTexts.Length)
            {
                Debug.Log("About to check !messagePlayed - " + messagePlayed);
                if (!messagePlayed)
                {
                    onTextExhausted.Raise(this, null); // NEED TO ESNURE REPEATABLE DOESN'T FIRE THIS AGAIN
                    Debug.Log("Found as !messagePlayed - Called onTextExhausted");
                }
                messagePlayed = true;
                ResetTextField();
            }
            if (textNumber < popupTexts.Length && fieldIsActive)
            {
                textField.text = popupTexts[textNumber];
                textNumber++;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == false)
        {
            textField.enabled = true;
            if (textNumber == 0)
            {
                textField.text = initialMessage;
                messagePlayed = false; //NEW TRY
            }
            fieldIsActive = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == true)
        {
            ResetTextField();
        }
    }

    public void ResetTextField()
    {
        if (isRepeatableMessage)
        {
            textNumber = 0;
            textField.text = initialMessage;
        }
        else
        {
            textField.text = ("");
        }

        textField.enabled = false;
        fieldIsActive = false;

    }

    public void ResetPopupTexts(int length)
    {
        Array.Resize<string>(ref popupTexts, length);
        popupTexts = new string[length];
        //Debug.Log("RESIZE TO: " + popupTexts.Length);
    }

}
