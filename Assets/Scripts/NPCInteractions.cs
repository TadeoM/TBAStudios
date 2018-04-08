using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractions : MonoBehaviour
{
    int value;
    public int convoIndex = 0;
    private bool isTriggered = false;
    private string currentMinigame;
    public string[] possibleMinigames;
    [SerializeField] private float happiness;
    private float maxHappiness = 100;
    
    private SpriteRenderer silhouette;
    private Transform child;
    private string[,] conversations;
    private string[] currentConversation;

    public string[] CurrentConversation
    {
        get { return currentConversation; }
        set { currentConversation = value; }
    }


    public string[,] Conversations
    {
        get { return conversations; }
        set { conversations = value; }
    }


    public float Happiness
    {
        get { return happiness; }
        set { happiness = value; }
    }

    public string CurrentMinigame
    {
        get { return currentMinigame; }
    }

    public bool IsTriggered
    {
        get { return isTriggered; }
    }


    // Use this for initialization
    void Awake()
    {
        happiness = 0;
        child = transform.GetChild(0);
        silhouette = child.GetComponent<SpriteRenderer>();
        ChangeMentalState(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Debug.Log(this.name);

        if (this.name == "ElizabethNPC" && !GameManager.Instance.playedE)
        {
            LevelManager.Instance.LoadScene(Level.CatMiniGame);
        }
        else if (this.name == "JoeJonesNPC" && !GameManager.Instance.playedJoe)
        {
            LevelManager.Instance.LoadScene(Level.HoardingMiniGame);
        }
        else if (this.name == "FuyumiNPC" && !GameManager.Instance.playedF)
        {
            LevelManager.Instance.LoadScene(Level.DialogMiniGame);
        }
        else if (this.name == "JonDoeNPC" && !GameManager.Instance.playedJon)
        {
            LevelManager.Instance.LoadScene(Level.CuttingMiniGame);
        }

        //LevelManager.Instance.LoadScene(Level.MainGame);


        isTriggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
    }
    public void NewDaySetup()
    {
        value = Random.Range(0, possibleMinigames.Length);

        currentMinigame = possibleMinigames[value];

        currentConversation = new string[conversations.GetUpperBound(1)];
        //Debug.Log(conversations.Length);
        for (int i = 0; i < currentConversation.Length; i++)
        {
            //Debug.Log(value);
            currentConversation[i] = conversations[0, i];
        }



    }

    public void ChangeMentalState(int hapVal)
    {
        happiness += hapVal * 10;
        if (happiness < 0)
        {
            happiness = 0;
        }
        else if(happiness > 100)
        {
            happiness = 100;
        }
        silhouette.color = new Color(silhouette.color.r, silhouette.color.g, silhouette.color.b, happiness / maxHappiness);
    }
}
