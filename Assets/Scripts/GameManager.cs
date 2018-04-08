using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector2 playerPos;
    private int cookingScore;

    public enum Kindness { Neutral, Ok, Good, Best };
    
    public int CookingScore
    {
        get
        {
            return cookingScore;
        }
        set
        {
            cookingScore = value;
        }
    }
    private NPCInteractions currentNPCScript;

    public NPCInteractions CurrentNPCScript
    {
        get
        {
            return currentNPCScript;
        }
        set
        {
            currentNPCScript = value;
        }
    }

    //Custom get set
    public Vector2 PlayerPos {
        get
        {
            return playerPos;
        }
        set
        {
            playerPos = value;
        }
    }   

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        cookingScore = 0;
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
