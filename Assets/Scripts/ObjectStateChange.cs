using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectStateChange : MonoBehaviour
{
    public GameObject objectToChange;
    public Material originalMaterial;
    public Material newMaterial;
    public Collider colliderToChange;

    public void UseNewMaterial()
    {
        objectToChange.GetComponent<Renderer>().material = newMaterial;
    }

    public void UseOriginalMaterial()
    {
        objectToChange.GetComponent<Renderer>().material = originalMaterial;
    }

    public void MakeInvisibile()
    {
        objectToChange.GetComponent<Renderer>().enabled = false;
    }

    public void MakeVisibile()
    {
        objectToChange.GetComponent<Renderer>().enabled = true;
    }

    public void TurnColliderOff()
    {
        colliderToChange.enabled = false;
    }

    public void TurnColliderOn()
    {
        colliderToChange.enabled = true;
    }

    public void KillThisObject()
    {
        Destroy(objectToChange);
    }

}
