using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityStandardAssets._2D;

public class MainController : MonoBehaviour
{

    public GameObject player;
    public GameObject[] nonPlayers;
    public Dialogues dialogue;
    public GameObject camera;
    private int daysLeft = 7;
    private int fadeTimer;
    private bool fadeOut;
    private int callOnce;
    private bool setupOnce = false;
    private NPCInteractions[] nonPlayerScripts;
    private Timer timer = new Timer();
    private GameObject[] interactableEnvironment;
    private SpriteRenderer cameraChild;


    // text stuff


    /// <summary>
    /// level of happiness of NPC
    /// your level of happiness
    /// how many days are left
    /// </summary>

    // Use this for initialization
    void Awake()
    {
        callOnce = 5;
        fadeTimer = 60;
        fadeOut = false;
        dialogue = GetComponent<Dialogues>();
        GetComponent<Dialogues>().SetupEverything();
        player = GameObject.FindGameObjectWithTag("Player");
        SetNonPlayers();
        dialogue.SetupEverything();
        cameraChild = camera.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer.Update();
        NPCInteraction();
        if (callOnce > 0)
            callOnce--;
        if (dialogue.isSetup && !setupOnce)
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
                Debug.Log(nonPlayers[i].gameObject.name + " We want to play " + nonPlayerScripts[i].CurrentMinigame + " minigame");
                GameManager.Instance.CurrentNPCScript = nonPlayerScripts[i];
            }
        }
    }

    public void UseElevator()
    {
        if (CheckInputs() == 0 && callOnce == 0)
        {
            Debug.Log(player.GetComponent<Animator>().speed);
            Fade(0.05f);
            int fadeTimer = 3000;
            while (fadeTimer > 0)
            {
                fadeTimer--;
            }
            if (player.transform.position.y < -6.57f)
            {
                player.transform.position = new Vector2(1f, -5.200848f);
                fadeTimer = 60;
            }
            else
            {
                player.transform.position = new Vector2(1f, -6.547f);
            }

            //Fade(0.1f);
            callOnce = 5;
        }


    }

    /// <summary>
    /// I need to fade in and out but my brain won't work
    /// I have a fadeTimer to call every frame
    /// I am making the player not able to make inputs while the fade is happening
    /// fadeOut should be true if you want to fade the screen to black and false if you want to fade the screen to white
    /// timer should be zero after the entire animation happens, and once the animation is done, the player can make inputs again
    /// </summary>
    /// <param name="upOrDown"></param>
    void Fade(float upOrDown)
    {
        Debug.Log("We in fade");
        Sequence fadeSequence = DOTween.Sequence();

        fadeSequence
            .Append(cameraChild.DOFade(1, 1))
            .Append(cameraChild.DOFade(0, 1))
        ;
        fadeSequence.Play();
        /*Debug.Log("Fading");
        if (upOrDown != 0.0f || upOrDown <= 1.0f)
        {
            cameraChild.color = new Color(0, 0, 0, cameraChild.color.a - upOrDown);
        }
        else
        {
            fadeOut = true;
        }
        fadeTimer--;*/
    }

    /// <summary>
    /// check inputs for user
    /// </summary>
    int CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Returning 0");
            return 0;
        }

        //Debug.Log("Not returning 0");

        return -1000;
    }
}
