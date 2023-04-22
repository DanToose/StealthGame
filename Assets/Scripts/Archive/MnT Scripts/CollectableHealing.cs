using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHealing : MonoBehaviour
{
    public GameObject PlayerEntity;

    // Start is called before the first frame update
    void Start()
    {
        PlayerEntity = GameObject.FindGameObjectWithTag("Player");
        if (PlayerEntity == null)
        {
            Debug.Log("WARNING - No Player assigned for Collectable Healing!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("COLLISION WITH Plank!");
            PlayerEntity.GetComponent<PlayerInventory>().AddHealing();
            Destroy(gameObject);
        }
    }
}
