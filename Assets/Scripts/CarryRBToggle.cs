using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRBToggle : MonoBehaviour
{
    public Rigidbody rb;
    public Collider thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisCollider = rb.GetComponent<Collider>();
    }

    void ToggleCarryObject()
    {
        rb.useGravity = !rb.useGravity;
        thisCollider.enabled = !thisCollider.enabled;
    }
}
