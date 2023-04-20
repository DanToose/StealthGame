using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundToPlay;
    public bool remoteSoundEffect; //Check this in Inspector if you want this played elsewhere
    public GameObject remoteSFXLocation;
    private Vector3 remoteObjectLoc;
    public AudioSource sourceToPlay; // THIS NEEDS TO BE AN AUDIOSOURCE COMPONENT IN YOUR LEVEL! Maybe 'SFXSytem'
    public float volume;
    public bool destructible;

    private void Start()
    {
        if (sourceToPlay == null)
        {
            sourceToPlay = GameObject.Find("SFXSystem").GetComponent<AudioSource>();
        }
        
        if (remoteSFXLocation == null)
        {
            remoteSoundEffect = false;
        }
        else
        {
            remoteObjectLoc = remoteSFXLocation.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (remoteSoundEffect)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, remoteObjectLoc); // THIS PLAYS IT AT THE ITEM PICKUP LOCATION
            }
            else
            {
                PlaySoundClip();
            }
            // If this is a pickup, you'd TOTALLY also do something here that shows the player has picked it up - Maybe flag a boolean in another script, etc
            
            if (destructible)
            {
                Destroy(gameObject); 
            }
                  
        }
    }

    public void PlaySoundClip()
    {
        sourceToPlay.PlayOneShot(soundToPlay, volume); //THIS PLAYS IT AT THE PLAYER LOCATION
    }

}
