using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicInteract : MonoBehaviour
{

    private BasicInventory playerInventory;
    private Ray g_ray = new Ray();
    public RaycastHit hitObject;

    [Header("Raycast Settings")]
    public LayerMask layerToHit;
    public GraphicRaycaster raycaster;
    public float rayLength = 5f;
    public Image CrosshairDot;

    [Header("Results")]
    public bool rayHit;
    public bool canInteract; // a blanket check of if you can do SOMETHING with an object under the crosshair.
    public GameObject interactiveObject; // what it is your crosshair is on
    public bool targetIsInteractive; // is the thing an interactive device?
    public bool targetIsCollctable; // is the thing a collectable object?
    public bool targetIsCarryable; // is the thing something to pick and drop?

    [Header("Prompt Options")]
    public bool interactablePromptsText; // These are all a checkbox to see IF the player wants a popup or not.
    public Text intPromptTxt;
    public string intMessage;
    public bool collectablePromptsText;
    public Text collectPromptTxt;
    public string collectMessage;
    public bool carryablePromptsText;
    public Text carryPromptTxt;
    public string carryMessage;
    public string dropMessage;



    //private bool nearInteractable;
    //private int numberInteractables;

    private GameObject carriedItem;
    public Transform carryPoint;
    private Transform carryItemPosition;
    private Rigidbody rbOfCarriedItem;



    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GetComponent<BasicInventory>();
        rayHit = false;
        canInteract = false;
        //numberInteractables = 0;
        CrosshairDot = GameObject.Find("CrosshairDot").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carriedItem != null)
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                DropObject();
            }
        }

        g_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast From Mouse Position if player is near an ally

        if (Physics.Raycast(g_ray, out hitObject, rayLength, layerToHit)) // If raycast hits collider 
        {
            if (hitObject.collider.tag == "Interact") // if raycast hits collider with tag - Implies a switch / device, not an object to pick up
            {
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                targetIsInteractive = true;
                if (interactablePromptsText)
                {
                    intPromptTxt.enabled = true;
                    intPromptTxt.text = intMessage;
                }

            }
            else if (hitObject.collider.tag == "Collectable") // if raycast hits collider with tag - Implies thing to pick up and keep
            {
                rayHit = true;
                targetIsCollctable = true;
                interactiveObject = hitObject.collider.gameObject;
                if (collectablePromptsText)
                {
                    collectPromptTxt.enabled = true;
                    collectPromptTxt.text = collectMessage;
                }
            }

            else if (hitObject.collider.tag == "Carryable") // if raycast hits collider with tag - Implies thing to carry and put down.
            {
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                targetIsCarryable = true;
                carryItemPosition = interactiveObject.transform;
                if (carryablePromptsText)
                {
                    carryPromptTxt.enabled = true;
                    carryPromptTxt.text = carryMessage;
                }

            }

        }
        else // resets Raycast
        {
            rayHit = false;
            interactiveObject = null;
            canInteract = false;
            targetIsCarryable = false;
            targetIsCollctable = false;
            targetIsInteractive = false;
            collectPromptTxt.enabled = false;
            intPromptTxt.enabled = false;

            //Debug.Log("allyToDirect = null");
        }

        GameObject.Find("CrosshairDot").GetComponent<Image>();

        if (rayHit == true) //Turns the Crosshair green
        {
            //Debug.Log("RaycastHit");
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.green;
            canInteract = true;
        }

        if (rayHit == false) // resets colour of crosshair
        {
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.white;
            canInteract = false;
        }


        // THIS IS WHERE ACTUAL INTERACTIONS HAPPEN, AND RESULT IN CALLING FUNCTIONS APPROPRIATE FOR THAT.

        if (canInteract == true)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && (interactiveObject != null))
            {
                // interactiveObject.GetComponent<LightSwitch>().lightSwitchToggle(); - OLD SETUP FOR LIGHT SWITCH
                if (targetIsCarryable)
                {

                    CarryObject();
                }
                else if (targetIsCollctable)
                {
                    playerInventory.AddInvItem(interactiveObject);
                    Destroy(interactiveObject);
                    //interactiveObject.GetComponent<PickupThing>().RemoveObject();
                }


            }
        }



    }

    void CarryObject()
    {
        Debug.Log("CarryObject called!");
        rbOfCarriedItem = interactiveObject.GetComponent<Rigidbody>();
        rbOfCarriedItem.useGravity = false; 
        carriedItem = interactiveObject;
        carryItemPosition.position = carryPoint.position;
        carryItemPosition.parent = carryPoint;
        carriedItem.tag = ("Untagged");
        interactiveObject = null;

        carryPromptTxt.text = dropMessage;

    }

    void DropObject()
    {
        Debug.Log("Drop object called!");
        carriedItem.tag = ("Carryable");
        rbOfCarriedItem.useGravity = true;
        carryItemPosition.parent = null;
        carriedItem = null;

        carryPromptTxt.text = " ";

    }



}
