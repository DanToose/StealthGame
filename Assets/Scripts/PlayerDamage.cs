using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public float health = 100.0f;
    public float timer = 0.0f;
    public GameObject painEffect;
    public float painTimer = 0.0f;
    public float damageMultiplier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timer = timer + Time.deltaTime;
            painTimer = painTimer - Time.deltaTime;
            if (timer >= 0.05)
            {
                health = health - 1 * damageMultiplier;
                timer = 0.0f;
            }

            if (painTimer <= 0.0)
            {
                Instantiate(painEffect, transform.position, Quaternion.identity);
                painTimer = 1.0f;
            }
        }
    }
}
