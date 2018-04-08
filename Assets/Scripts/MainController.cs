    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private int daysLeft = 7;
    public GameObject player;
    public GameObject[] nonPlayers;
    public Dialogues dialogue;
    bool setupOnce = false;
    NPCInteractions[] nonPlayerScripts;
    private Timer timer = new Timer();

    // text stuff


    /// <summary>
    /// level of happiness of NPC
    /// your level of happiness
    /// how many days are left
    /// </summary>

	// Use this for initialization
	void Awake () {
        dialogue = GetComponent<Dialogues>();
        GetComponent<Dialogues>().SetupEverything();
        player = GameObject.FindGameObjectWithTag("Player");
        SetNonPlayers();
        dialogue.SetupEverything();
    } 
	
	// Update is called once per frame
	void FixedUpdate () {
        timer.Update();
        NPCInteraction();
        if(dialogue.isSetup && !setupOnce)
        {
            NewDay();
            setupOnce = true;
        }
        if (!timer.running)
        {
            switch (daysLeft)
            {
                case 0:
                    Debug.Log("Game is Over!");
                    Time.timeScale = 0;
                    break;
                default:
                    NewDay();
                    break;
            }
        }

        
    }

    /// <summary>
    /// Find the NPC characters
    /// </summary>
    void SetNonPlayers()
    {
        nonPlayers = GameObject.FindGameObjectsWithTag("NPC");

        nonPlayerScripts = new NPCInteractions[nonPlayers.Length];

        // for every NPC we found, get their script
        for (int i = 0; i < nonPlayers.Length; i++)
        {
            nonPlayerScripts[i] = nonPlayers[i].GetComponent<NPCInteractions>();
        }
    }

    /// <summary>
    /// Sets all values for a new day
    /// </summary>
    void NewDay()
    {
        int timePerDay = 10;
        timer.SetTimer(timePerDay);
        timer.StartTimer();
        SetNonPlayers();

        for (int i = 0; i < nonPlayerScripts.Length; i++)
        {
            nonPlayerScripts[i].NewDaySetup();
        }
        Debug.Log("New day");
        daysLeft--;
    }

    /// <summary>
    /// check if the player will interact with the NPC
    /// </summary>
    void NPCInteraction()
    {
        for (int i = 0; i < nonPlayers.Length; i++)
        {
            if (nonPlayerScripts[i].IsTriggered && CheckInputs() == 0)
            {
                Debug.Log("We want to play " + nonPlayerScripts[i].CurrentMinigame + " minigame");
            }
        }
    }

    /// <summary>
    /// check inputs for user
    /// </summary>
    int CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            return 0;
        }

        return -1000;
    }
}
