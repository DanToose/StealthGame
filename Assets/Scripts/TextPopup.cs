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
        keyWasPressed = Input.GetKeyDown(keyToAdvanceText);

        if (fieldIsActive == true && keyWasPressed)
        {
            if (textNumber >= popupTexts.Length)
            {
                if (!messagePlayed)
                {
                    onTextExhausted.Raise(this, null); // NEED TO ESNURE REPEATABLE DOESN'T FIRE THIS AGAIN
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == false)
        {
            textField.enabled = true;
            if (textNumber == 0)
            {
                textField.text = initialMessage;
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
}
