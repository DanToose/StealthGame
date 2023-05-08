using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithObject : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        if (player == null) // If player is not assigned in the Inspector, find it based on tag.
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.SetParent(transform); // ties the player's transform to this object's transform.
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.SetParent(null); // unties the player's transform from this object's transform.
        }
    }

}
