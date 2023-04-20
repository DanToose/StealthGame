using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableThing : MonoBehaviour
{
    public string objectiveKey;
    //public int objectiveNumber; // Set this between 0 and the last objective index
    public bool showOnHud;
    public Text HUDText;
    public bool imageIndicator;
    public Image HUDImage;
    
    // Start is called before the first frame update
    void Start()
    {
        if (HUDText == null)
        {
            HUDText = GameObject.Find("Text").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //StaticVariables._instance.objectiveSet[objectiveNumber] = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StaticVariables._instance.UpdateObjective(objectiveKey, true); // change true tp 'objectiveValue' if you want to allow unsetting

            if (showOnHud)
            {
                if (!imageIndicator)
                {
                    if (HUDText.gameObject.activeSelf == false)
                    {
                        HUDText.gameObject.SetActive(true);
                    }
                    else
                    {
                        HUDText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (HUDImage.gameObject.activeSelf == false)
                    {
                        HUDImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        HUDImage.gameObject.SetActive(false);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
