using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    private string initialMessage;
    public string[] popupTexts;
    public KeyCode keyToAdvanceText;
    private int textNumber;
    private bool fieldIsActive;
    public Text textField;
    public GameObject textFieldObject;
    private bool keyWasPressed;


    // Start is called before the first frame update
    void Start()
    {
        textFieldObject = GameObject.Find("PopupText");
        textField = textFieldObject.GetComponent<Text>();
        initialMessage = textField.text;
        fieldIsActive = false;
        textFieldObject.SetActive(false);
        textNumber = 0;
    }

    // Update is called once per frame

    private void Update()
    {
        keyWasPressed = false;
        keyWasPressed = Input.GetKeyDown(keyToAdvanceText);

        if (fieldIsActive == true && keyWasPressed)
        {
            Debug.Log("TN = " + textNumber);
            
            if (textNumber >= popupTexts.Length)
            {              
                ResetTextField();
            }
            if (textNumber < popupTexts.Length)
            {
                textField.text = popupTexts[textNumber];
                textNumber++;
                //keyWasPressed = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == false)
        {
            textFieldObject.gameObject.SetActive(true);
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

    private void ResetTextField()
    {
        Debug.Log("Reset Text Field called");
        textNumber = 0;
        textField.text = initialMessage;
        textField.gameObject.SetActive(false);
        fieldIsActive = false;
    }
}
