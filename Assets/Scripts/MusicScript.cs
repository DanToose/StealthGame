using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip musicOne;
    [SerializeField]
    private AudioClip musicTwo;
    [SerializeField]
    private AudioClip musicThree;
    [SerializeField]
    private AudioSource soundSource;


    // Start is called before the first frame update
    void Start()
    {
        soundSource.clip = musicOne;
        soundSource.loop = true;
        soundSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
