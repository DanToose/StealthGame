using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Runtime.CompilerServices;

[Serializable]
public class IntEvent : UnityEvent<int> { }

public class BasicInteract : MonoBehaviour
{

    private InventorySystem playerInventory;

    // RAYCAST VARS
    [Header("Raycast Settings")]
    private Ray g_ray = new Ray();
    public RaycastHit hitObject;
    public LayerMask layerToHit;
    //public GraphicRaycaster raycaster;
    public float rayLength = 5f; // Adjust this if you want to adjust the 'click to grab' distance
    public Image CrosshairDot;

    // VARS FOR RAYCAST RESULTS - ALL ABOUT SHOWING BOOL RESULTS, NOT SETTING THEM!
    [Header("Results")]
    public bool rayHit;
    public bool canInteract; // a blanket check of if you can do SOMETHING with an object under the crosshair.
    public GameObject interactiveObject; // what it is your crosshair is on
    public bool targetIsInteractive; // is the thing an interactive device?
    public bool targetIsCollctable; // is the thing a collectable object?
    public bool targetIsCarryable; // is the thing something to pick and drop?

    public IntEvent onInvItemTaken;

    // PROMPT SYSTEM VARS - All about options for showing prompts, and also for what text is shown when they fire.
    [Header("Prompt Options")]
    public KeyCode interactOrPickUpKey;
    public KeyCode dropKey;
    public bool useInventory;
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

    // CARRIED ITEM VARS
    private GameObject carriedItem;
    public Transform carryPoint;
    private Transform carryItemPosition;
    private Rigidbody rbOfCarriedItem;


    void Start()
    {
        if (useInventory)
        {
            playerInventory = GameObject.Find("InventoryManager").GetComponent<InventorySystem>();
        }
        rayHit = false;
        canInteract = false;
        CrosshairDot = GameObject.Find("CrosshairDot").GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1)) // DIRTY HACK! JUST TESTS DROPPING/USING AN ITEM.
        {
            int itemToDiscard = 0;
            playerInventory.RemoveItem(itemToDiscard);

        }
        
        if (carriedItem != null) // IF YOU ARE CARRYING SOMETHING, CHECKS FOR AN INPUT TO DROP IT.
        {
            if (Input.GetKeyUp(dropKey))
            {
                DropObject();
            }
        }

        g_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast From Mouse Position 
        if (Physics.Raycast(g_ray, out hitObject, rayLength, layerToHit)) // If raycast hits collider... 
        {
            if (hitObject.collider.tag == "Interact") // If that collider has 'Interact' tag - Implies a switch / device, not an object to pick up
            {
                // THIS STUFF IS ABOUT FIRING PROMPTS FOR SOMETHING YOU CAN INTERACT WITH IN THE WORLD
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                targetIsInteractive = true;
                if (interactablePromptsText)
                {
                    intPromptTxt.enabled = true;
                    intPromptTxt.text = intMessage;
                }

            }
            else if (hitObject.collider.tag == "Collectable") // If that collider has 'Collectable' tag - Implies thing to pick up and keep
            {
                // THIS STUFF IS ABOUT FIRING PROMPTS FOR SOMETHING YOU CAN TAKE TO INVENTORY
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
                // THIS STUFF IS ABOUT FIRING PROMPTS FOR SOMETHING YOU CAN CARRY
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
        else // IF RAYCAST DOESN'T HIT ONE OF THOSE THINGS, RESET RAYCAST AND DEACTIVATE ALL PROMPTS
        {
            //ResetPrompts();

            rayHit = false;
            interactiveObject = null;
            canInteract = false;
            targetIsCarryable = false;
            targetIsCollctable = false;
            targetIsInteractive = false;
            collectPromptTxt.enabled = false;
            intPromptTxt.enabled = false;
            if (carriedItem == null)
            {
                carryPromptTxt.enabled = false;
            }
        }

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
            if (Input.GetKeyUp(interactOrPickUpKey) && (interactiveObject != null))
            {
                if (targetIsCarryable)
                {
                    CarryObject(); // PICKS IT UP, THAT'S IT.
                }
                else if (targetIsCollctable)
                {
                    if (useInventory)
                    {
                        onInvItemTaken?.Invoke(interactiveObject.GetComponent<InvItemID>().ID); // ADD TO INVENTORY LIST
                        interactiveObject.GetComponent<PickupThing>().CollectionEvent();
                    }

                    Destroy(interactiveObject); // REMOVE OBJECT FROM THE WORLD
                }
                else if (targetIsInteractive)
                {
                    interactiveObject.GetComponent<Interactable>().TriggerEvent(); // ACTIVE THE 'TriggerEvent' FUNCTION ON THE OBJECT.
                }
            }
        }
    }

    // TWO FUNCTIONS FOR CARRYABLE OBJEECTS ONLY
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

        //ResetPrompts();

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

    void ResetPrompts()
    {
        rayHit = false;
        interactiveObject = null;
        canInteract = false;
        targetIsCarryable = false;
        targetIsCollctable = false;
        targetIsInteractive = false;
        collectPromptTxt.enabled = false;
        intPromptTxt.enabled = false;
        if (carriedItem == null)
        {
            carryPromptTxt.enabled = false;
        }
    }

    public void RunoverPickup(GameObject pickedUp)
    {
        onInvItemTaken?.Invoke(pickedUp.GetComponent<InvItemID>().ID); // ADD TO INVENTORY LIST
    }

}
