using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isStartpoint;
    public bool isStartpointOnly; // Only set this to true is you don't want the player to be able to return to their original startpoint to make it the CP again.
    public bool isCheckpoint;
    //public GameObject player; OLD
    public GameObject gameManager;
    public GameObject oldCheckpoint;
    public Respawner respawn;

    // Start is called before the first frame update
    void Start()
    {
        isCheckpoint = false;
        if (isStartpoint)
        {
            if (gameObject.tag != "StartPoint")
            {
                gameObject.tag = "StartPoint";
            }
        }

        gameManager = GameObject.Find("GameManager");
        respawn = gameManager.GetComponent<Respawner>();
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
