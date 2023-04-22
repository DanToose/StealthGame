using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePlank : MonoBehaviour
{
    public GameObject PlayerEntity;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerEntity = GameObject.FindGameObjectWithTag("Player");
        if (PlayerEntity == null)
        {
            Debug.Log("WARNING - No Player assigned for Collectable Plank!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("COLLISION WITH Plank!");
            PlayerEntity.GetComponent<PlayerInventory>().AddPlank();
            Destroy(gameObject);
        }
    }
}
