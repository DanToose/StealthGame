using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public Text healthText;
    public float playerMaxHealth = 100;

    // THIS SCRIPT ALSO CONTAINS RESPAWN INFO FOR THE PLAYER
    //public GameObject currentCheckpoint;
    public float respawnDelay = 3.0f;
    public Respawner respawn;
    public bool playerIsAlive;
    private bool readyToRespawn;

    public CharacterController charController;

    public Text deathText;
    public Text respawnText;
    public Image deathPanel;

    public FPSLightCheck lightCheck;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure components are assigned
        healthText = GameObject.Find("Health").GetComponent<Text>();
        deathText = GameObject.Find("DeathText").GetComponent<Text>();
        respawnText = GameObject.Find("RespawnText").GetComponent<Text>();
        deathPanel = GameObject.Find("DeathPanel").GetComponent<Image>();
        lightCheck = GameObject.Find("lightChecker").GetComponent<FPSLightCheck>();
        charController = GetComponent<CharacterController>();

        playerHealth = playerMaxHealth;
        healthText.text = "Health: " + playerHealth;
        playerIsAlive = true;
        respawnText.text = "";
        deathText.text = "";
        deathPanel.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + playerHealth;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                playerDeath();
                playerIsAlive = false;
            }
        }

        if (readyToRespawn && Input.anyKeyDown)
        {
            readyToRespawn = false;
            respawnText.text = "";
            deathText.text = "";
            deathPanel.gameObject.SetActive(false);
            lightCheck.GetComponent<FPSLightCheck>().isVisible = false;
            ActualRespawn();
        }
    }

    public void playerDeath()
    {
        // death stuff
        deathPanel.gameObject.SetActive(true);
        deathText.text = "You Died!";
        playerIsAlive = false;

        // CHAR CONTROLLER SHOULD STOP AT THIS POINT
        charController.enabled = false;

        Invoke("RespawnFromDeath", respawnDelay);

    }

    void RespawnFromDeath()
    {
        respawnText.text = "Press any key to respawn";
        readyToRespawn = true;
    }

    void ActualRespawn()
    {
        playerHealth = playerMaxHealth;
        playerIsAlive = true;
        respawn.RespawnPlayer();
    }
}
