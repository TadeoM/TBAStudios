using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityStandardAssets._2D;

public class MainController : MonoBehaviour
{
    public GameObject textBox;
    public GameObject player;
    public GameObject[] nonPlayers;
    public Dialogues dialogue;
    public GameObject camera;
    private bool displayText;
    private bool inConversation;
    private int daysLeft = 7;
    private int fadeTimer;
    private int currentNPC;
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
        displayText = false;
        inConversation = false;
        callOnce = 5;
        fadeTimer = 60;
        fadeOut = false;
        dialogue = GetComponent<Dialogues>();
        GetComponent<Dialogues>().SetupEverything();
        player = GameObject.FindGameObjectWithTag("Player");
        SetNonPlayers();
        dialogue.SetupEverything();
        cameraChild = camera.transform.GetChild(0).GetComponent<SpriteRenderer>();
        textBox.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer.Update();
        if(timer.seconds <= 0)
        {
            timer.running = false;
        }
        NPCInteraction();
        if (callOnce > 0)
            callOnce--;
        if (dialogue.isSetup && !setupOnce)
        {
            NewDay();
            setupOnce = true;
        }

        ConversationMethod();
        

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

    void ConversationMethod()
    {
        if (inConversation)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(nonPlayerScripts[currentNPC].convoIndex % 2);
                // textCanvas.get
                textBox.SetActive(true);
                textBox.GetComponentInChildren<TextMeshProUGUI>().text = nonPlayerScripts[currentNPC].CurrentConversation[nonPlayerScripts[currentNPC].convoIndex];
                if (nonPlayerScripts[currentNPC].convoIndex % 2 == 0)
                {
                    textBox.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255);
                }
                else
                {
                    textBox.GetComponentInChildren<TextMeshProUGUI>().color = new Color(53 / 255, 53 / 255, 255 / 255);
                    //textBox.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0);
                }



                nonPlayerScripts[currentNPC].convoIndex++;
                if (nonPlayerScripts[currentNPC].convoIndex > nonPlayerScripts[currentNPC].CurrentConversation.Length - 1)
                {
                    nonPlayerScripts[currentNPC].convoIndex = 0;
                    inConversation = false;
                    player.GetComponent<Platformer2DUserControl>().acceptInput = true;
                    player.GetComponent<Animator>().speed = 1;
                    textBox.SetActive(false);
                }

            }
        }
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
                currentNPC = i;
                inConversation = true;
                player.GetComponent<Platformer2DUserControl>().acceptInput = false;
                player.GetComponent<Animator>().speed = 0;
                //for (int word = 0; word < nonPlayerScripts[i].Conversations.Length; word++)
                //{
                //    nonPlayerScripts[i].CurrentConversation[word] = nonPlayerScripts[i].Conversations[0, word];
                //}

                
                //Debug.Log(nonPlayers[i].gameObject.name + " We want to play " + nonPlayerScripts[i].CurrentMinigame + " minigame");
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

            if (player.transform.position.y < -6.57f)
            {
                player.transform.position = new Vector2(1f, -5.20f);
                fadeTimer = 60;
            }
            else if (player.transform.position.x <= -7.5f && player.transform.position.y < -4.5f)
            {
                player.transform.position = new Vector2(-7.5f, -3f);
            }
            else if (player.transform.position.x <= -7.5f && player.transform.position.y < -2.971f)
            {
                player.transform.position = new Vector2(-7.5f, -5.20f);
            }
            else if (player.transform.position.x >= -2.384f && player.transform.position.y < -2.971f)
            {
                // -2.49  -0.9655123
                player.transform.position = new Vector2(-2.49f, -0.96f);
            }
            // this elseif is broken
            else if (player.transform.position.x <= -1.7f && player.transform.position.y > -1f)
            {
                // -2.49  -0.9655123
                player.transform.position = new Vector2(-2.384f, -2.971f);
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
            .Append(cameraChild.DOFade(1, 0.25f))
            .Append(cameraChild.DOFade(0, 0.5f))
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
            //Debug.Log("Returning 0");
            return 0;
        }
        else if(Input.GetKey(KeyCode.F))
        {
            return 1;
        }

        //Debug.Log("Not returning 0");

        return -1000;
    }
}
