using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    float seconds;
    float startTime;

    public bool running;

    public delegate void SecondsChanged(int seconds);
    public event SecondsChanged OnSecondsChanged;

    public delegate void TimeUp();
    public event TimeUp OnTimeUp;

    float secondsTracker;
	
	// Update is called once per frame
	public void Update () {
        if (running)
        {
            seconds -= Time.deltaTime;
            seconds = Mathf.Clamp(seconds, 0, seconds);

            if(secondsTracker - seconds >= 1 && OnSecondsChanged != null)
            {
                OnSecondsChanged((int)Mathf.Ceil(seconds));
            }

            if (seconds <= 0)
            {
                TimeExpired();
            }
        }        
	}

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void SetTimer(int seconds, bool start=false)
    {
        this.seconds = seconds;
        startTime = seconds;
        secondsTracker = seconds;
        if (start) StartTimer();
    }

    public void SetTimer(float seconds, bool start = false)
    {
        this.seconds = seconds;
        startTime = seconds;
        secondsTracker = seconds;
        if (start) StartTimer();
    }

    public void Reset()
    {
        seconds = startTime;
    }

    void TimeExpired()
    {
        StopTimer();
        if (OnTimeUp != null) OnTimeUp();
    }
}
