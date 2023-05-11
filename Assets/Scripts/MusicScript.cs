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
        ChangeMusic(1);

    }


    public void ChangeMusic(int tuneNumber)
    {
        if (tuneNumber == 1)
        {
            soundSource.clip = musicOne;
        }
        if (tuneNumber == 2)
        {
            soundSource.clip = musicTwo;
        }
        if (tuneNumber == 3)
        {
            soundSource.clip = musicThree;
        }

        //soundSource.clip = musicOne;
        soundSource.loop = true;
        soundSource.Play();
    }

    public void ToggleMusic()
    {
        if (soundSource.isPlaying)
        {
            soundSource.Stop();
        }
        else
        {
            soundSource.Play();
        }
    }

}
