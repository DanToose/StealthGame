using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isStartpoint;
    public bool isStartpointOnly;
    public bool isCheckpoint;
    //public GameObject player; OLD
    public GameObject gameManager;
    public GameObject oldCheckpoint;
    public Respawner respawn;

    // Start is called before the first frame update
    void Start()
    {
        isCheckpoint = false;
        //player = GameObject.FindGameObjectWithTag("Player");
        //respawn = player.GetComponent<Respawner>();

        gameManager = GameObject.Find("GameManager");
        respawn = gameManager.GetComponent<Respawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isStartpointOnly)
        {
            oldCheckpoint = gameManager.gameObject.GetComponent<Respawner>().currentCheckpoint;
            oldCheckpoint.GetComponent<CheckPoint>().isCheckpoint = false;
            
            isCheckpoint = true;
            gameManager.gameObject.GetComponent<Respawner>().currentCheckpoint = gameObject;

            respawn.UpdateCheckPoints();

        }
    }
}
