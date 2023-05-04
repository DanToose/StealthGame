using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTimer : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn;
    public Text timerText;
    public bool hideFromHUD;

    [Header("Events")]
    public GameEvent onTimerStarts;
    public GameEvent onTimerEnds;

    // Start is called before the first frame update
    void Start()
    {
        timerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                onTimerEnds.Raise(this, null);
                timerOn = false;
            }
        }
    }

    public void UpdateTimer(float currentTime)
    {
        // currentTime is passed in as an initial timer setting.
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void ActivateTimer()
    {
        if (!hideFromHUD)
        {
            timerText.enabled = true;
        }

        timerOn = true;
        onTimerStarts.Raise(this, null);
    }

    public void CancelTimer()
    {
        timerOn = false;
        timerText.text = "";
        timerText.enabled = false;
    }
}
