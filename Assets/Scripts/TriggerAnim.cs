using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    public GameObject thingToAnimate;
    public Animator animToPlay;
    public string triggerName;
    public bool playRepeatedly;
    public bool playedOnce;


    [Header("Events")]
    public GameEvent onEndAnim;

    public void PlayAnim()
    {
        thingToAnimate.GetComponent<Animator>().SetTrigger(triggerName);
    }

    public void PlayNextAnim()
    {
        // call something on a TARGET object.
        if (!playedOnce || playRepeatedly)
        {
            playedOnce = true;
            onEndAnim.Raise(this, null);
        }

    }
}
