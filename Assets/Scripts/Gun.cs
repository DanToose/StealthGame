using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameObject bulletSpawnPoint;
    public bool canFire;
    public float rateOfFire;
    GameObject player;
    private int ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletSpawnPoint = transform.GetChild(0).gameObject;
        canFire = true;
        ammoCount = player.gameObject.GetComponent<PlayerInventory>().fireSpellCount;
        Debug.Log("Ammo " + ammoCount);
    }

    // Update is called once per frame
    void Update()
    {
        fireShot();
    }

    public void fireShot()
    {
        ammoCount = player.gameObject.GetComponent<PlayerInventory>().fireSpellCount;

        if (Input.GetMouseButtonDown(0) && canFire && ammoCount > 0)
        {
            StartCoroutine(FireRate());
        }
         
        IEnumerator FireRate()
        {
            canFire = false;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            //ammoCount--;
            player.GetComponent<PlayerInventory>().LostSpell();
            yield return new WaitForSeconds(rateOfFire);
            canFire = true;
        }
            
    }
}
