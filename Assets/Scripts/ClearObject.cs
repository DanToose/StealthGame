using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", 5.0f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
