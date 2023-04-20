using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicInteract : MonoBehaviour
{

    private Ray g_ray = new Ray();
    public RaycastHit hitObject;

    [Header("Raycast Settings")]
    public LayerMask layerToHit;
    public GraphicRaycaster raycaster;
    public float rayLength = 5f;
    public Image CrosshairDot;

    [Header("Results")]
    public bool rayHit;
    public bool canInteract;
    public GameObject interactiveObject;

    [Header("Prompt Options")]
    public bool interactablePromptsText;
    public bool collectablePromptsText;
    public bool carryablePromptsText;

    //private bool nearInteractable;
    //private int numberInteractables;





    // Start is called before the first frame update
    void Start()
    {
        rayHit = false;
        canInteract = false;
        //numberInteractables = 0;
        CrosshairDot = GameObject.Find("CrosshairDot").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        g_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast From Mouse Position if player is near an ally

        if (Physics.Raycast(g_ray, out hitObject, rayLength, layerToHit)) // If raycast hits collider 
        {
            if (hitObject.collider.tag == "Interact") // if raycast hits collider with tag - Implies a switch / device, not an object to pick up
            {
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                //Debug.Log("Cursor over =" +interactiveObject);
            }
            if (hitObject.collider.tag == "Collectable") // if raycast hits collider with tag - Implies thing to pick up and keep
            {
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                //Debug.Log("Cursor over =" +interactiveObject);
            }

            if (hitObject.collider.tag == "Carryable") // if raycast hits collider with tag - Implies thing to carry and put down.
            {
                rayHit = true;
                interactiveObject = hitObject.collider.gameObject;
                //Debug.Log("Cursor over =" +interactiveObject);
            }

        }
        else // resets Raycast
        {
            rayHit = false;
            interactiveObject = null;
            canInteract = false;
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

        if (canInteract == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && (interactiveObject != null))
            {
                Debug.Log("E key registered!");
                interactiveObject.GetComponent<LightSwitch>().lightSwitchToggle();
            }
        }

    }

    
   /* OLD CODE FOR ALLY INTERACTION SYSTEM... KINDA CUTE, MIGHT DELETE LATER.
    public void AddInt()
    {
        numberInteractables++;
    }

    public void SubInt()
    {
        numberInteractables--;
    }
   */

}
