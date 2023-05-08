using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoOnEvent : MonoBehaviour
{
    public AudioSource sourceToPlay;
    public AudioClip eventSoundClip; // Pick an MP3, store them in a folder.
    public float eventClipVolume;
    public Text eventTextField; // Set this to a text field on a Canvas you want to display to 
    public string eventTextToShow;
    public float secondsToShow; // Duration of event text.
    public bool isRepeatable;
    private bool hasPlayed;
    private bool fieldWasInactive;

    [Header("Events")]
    public GameEvent onInfoDelivered;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
    }

    public void PlayEventFeedback()
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            if (eventSoundClip != null)
            {
                sourceToPlay.PlayOneShot(eventSoundClip, eventClipVolume);
            }

            if (eventTextToShow != null)
            {
                if (eventTextField.enabled == false)
                {
                    fieldWasInactive = true;
                    eventTextField.enabled = true;
                }
                eventTextField.text = eventTextToShow;
            }

            StartCoroutine(ClearText());
        }
    }

    IEnumerator ClearText()
    {        
        yield return new WaitForSeconds(secondsToShow);
        eventTextField.text = "";
        if (fieldWasInactive)
        {
            eventTextField.enabled = false;
        }
        if (isRepeatable)
        {
            hasPlayed = false;
        }

        if (onInfoDelivered != null)
        {
            onInfoDelivered.Raise(this, null);
        }
    }
}
