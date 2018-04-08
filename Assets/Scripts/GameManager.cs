using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector2 playerPos;
    public int cookingScore;

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
    }

    public void SetPlayerPos(Vector2 newPos)
    {
        playerPos = newPos;
    }

    public Vector2 GetPlayerPos()
    {
        return playerPos;
    }

    public void AddCookingScore(int score)
    {
        cookingScore += score;
    }

    public int GetCookingScore()
    {
        return cookingScore;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
