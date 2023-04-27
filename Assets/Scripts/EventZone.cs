using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    private GameObject player;
    private bool isDone;
    public bool isRepeatable;

    [Header("Events")]
    public GameEvent onTriggerEntered;

    // Start is called before the first frame update
    void Start()
    {
        isDone = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isDone)
        {
            if (!isRepeatable)
            {
                isDone = true;
            }
            onTriggerEntered.Raise(this, null);
        }
    }
}
