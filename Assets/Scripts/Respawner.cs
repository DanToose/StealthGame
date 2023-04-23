using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    public List<GameObject> CPList = new List<GameObject>();
    public GameObject currentCheckpoint;
    private Transform checkpointLocation;
    public GameObject player;
    private GameObject startingPoint;
    private CharacterController charController;
    private EnemyManager enemyManager;
    private GameObject lightChecker;
    private FPSLightCheck FPSlc;


    public Material activeMaterial;
    public Material inactiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        charController = player.gameObject.GetComponent<CharacterController>();
        enemyManager = GetComponent<EnemyManager>();
        lightChecker = GameObject.Find("lightChecker");
        FPSlc = lightChecker.GetComponent<FPSLightCheck>();

        startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
        if (currentCheckpoint == null)
        {
            startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
            currentCheckpoint = startingPoint;
        }
        currentCheckpoint = startingPoint;

        CPList.AddRange(GameObject.FindGameObjectsWithTag("CheckPoint"));

        InitialSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void InitialSpawn()
    {
        Debug.Log("St: " + startingPoint);
        player.gameObject.transform.position = startingPoint.gameObject.transform.position;
    }

    public void RespawnPlayer()
    {
        charController.enabled = false;

        enemyManager.ResetEnemies();

        checkpointLocation = currentCheckpoint.transform;
        player.transform.position = checkpointLocation.position;

        if (FPSlc != null)
        {
            FPSlc.VisibilityInitialised();
        }

        Invoke("ReactivateController", 0.5f);

    }

    public void UpdateCheckPoints()
    {
        Debug.Log("UpdateCheckPoints was called");
        foreach (GameObject l in CPList)
        {
            if (l.gameObject.GetComponent<CheckPoint>().isCheckpoint == false)
            {
                l.gameObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
            }
            else
            {
                l.gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
            }
        }
    }

    private void ReactivateController()
    {
        charController.enabled = true;
    }


    /* OLD CODE FOR CHECKPOINTS WITH LEVEL LOADING
    private void OnLevelWasLoaded(int level)
    {
        startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
        if (currentCheckpoint == null)
        {
            startingPoint = GameObject.FindGameObjectWithTag("StartPoint");
            currentCheckpoint = startingPoint;
        }
        currentCheckpoint = startingPoint;

        CPList.AddRange(GameObject.FindGameObjectsWithTag("CheckPoint"));

        InitialSpawn();

    }*/


}



