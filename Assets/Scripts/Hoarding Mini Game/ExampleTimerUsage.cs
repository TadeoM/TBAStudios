using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTimerUsage : MonoBehaviour {


    Timer timer;
	// Use this for initialization
	void Start () {
        // Create the timer
        timer = new Timer();
        // Register for the events
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;

        // Start the timer 
        timer.SetTimer(30);
        timer.StartTimer();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer.Update();
	}

    void HandleTimeUp()
    {
        Debug.Log("Time is up!");
    }

    void HandleSecondsChanged(int secondsRemaining)
    {
        Debug.Log("Seconds Remaining: " + secondsRemaining);
    }
}
