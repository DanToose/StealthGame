using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    private NavmeshAgentScript enemyNMA;

    // Start is called before the first frame update
    void Start()
    {
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void ResetEnemies()
    {
        foreach (GameObject e in enemyList)
        {
            enemyNMA = e.GetComponent<NavmeshAgentScript>();
            if (enemyNMA.jobIsPatrol)
            {
                enemyNMA.AIState = 3;
            }
            else
            {
                enemyNMA.AIState = 4;
            }

            e.transform.position = enemyNMA.wayPoints[0].transform.position;
            e.transform.rotation = enemyNMA.wayPoints[0].transform.rotation;
        }
        Debug.Log("All Enemies reset");
    }

    public void AllEnemyAlert()
    {
        foreach (GameObject e in enemyList)
        {
            // THIS WOULD BE ABOUT PUTTING ENEMIES INTO A NEW AI STATE (6) - WHERE THEY HEAD TO A LOCATION GIVEN TO THEM BY SOMETHING.

        }
    }


    // IF ENEMIES ARE REMOVED FROM THE GAME, A FUNCTION TO ADD/REMOVE ENEMIES FROM THE LIST MUST BE MADE
        
}
