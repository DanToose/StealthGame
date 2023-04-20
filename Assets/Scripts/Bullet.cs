using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 100.0f;
    public GameObject splatEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Invoke("DestroySelf", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(splatEffect, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject); //TEMP INSTAKILL
        }
        if (collision.gameObject.tag != "Player")
        {
            DestroySelf();
        }
 
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
