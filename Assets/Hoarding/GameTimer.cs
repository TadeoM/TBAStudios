using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

    private Timer timer;
    private int timeLeft;
    [SerializeField] private int GAME_TIME;

    public int TimeLeft
    {
        get { return timeLeft; }
    }

    public int GameTime
    {
        get { return GAME_TIME; }
    }

	// Use this for initialization
	void Start () {
        timeLeft = GAME_TIME;
        timer = new Timer();
        timer.OnSecondsChanged += HandleSecondsChanged;
        timer.SetTimer(GAME_TIME);
        timer.StartTimer();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer.Update();
	}


    void HandleSecondsChanged(int secondsRemaining)
    {
        timeLeft = secondsRemaining;
    }

}
