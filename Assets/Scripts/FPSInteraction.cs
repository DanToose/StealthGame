using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSInteraction : MonoBehaviour
{
    private Ray g_ray = new Ray();
    public RaycastHit hitAlly;
    public LayerMask layerToHit;
    public GraphicRaycaster raycaster;
    public float rayLength = 10f;

    public bool rayHit;

    public Image CrosshairDot;
    public bool nearAlly;
    public bool canInteract;
    public GameObject allyToDirect;

    // Start is called before the first frame update
    void Start()
    {
        rayHit = false;
        nearAlly = false;
        canInteract = false;
        CrosshairDot = GameObject.Find("CrosshairDot").GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        // FIRST SECTION OF ONUPDATE IS ABOUT CHECKING PROXIMITY TO ALLIES

        if (nearAlly == false)
        {
            // This section forces the crosshair to go 'normal' if the player is not near an ally to interact with.
            GameObject.Find("CrosshairDot").GetComponent<Image>();
            CrosshairDot.GetComponent<Renderer>();
            CrosshairDot.color = Color.white;
            canInteract = false;
        }

        if (nearAlly == true)
        {
            g_ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast From Mouse Position if player is near an ally

            if (Physics.Raycast(g_ray, out hitAlly, rayLength, layerToHit)) // If raycast hits collider 
            {
                if (hitAlly.collider.tag == "Ally") // if raycast hits collider with tag
                {
                    rayHit = true;
                    allyToDirect = hitAlly.collider.gameObject;
                    //Debug.Log("allyToDirect =" +allyToDirect);
                }
            }
            else // resets Raycast
            {
                rayHit = false;
                allyToDirect = null;
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
        }

        // SECOND SECTION OF ONUPDATE IS ABOUT ORDERING ALLIES TO FOLLOW
        if (canInteract == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && (allyToDirect != null))
            {
                Debug.Log("E key registered!");
                allyToDirect.GetComponent<AllyFollow>().followPlayerToggle();
            }
        }

    }
}