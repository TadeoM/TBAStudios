using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CatColor { Grey, Orange, Black, Brown, White }

public class CatMiniGame : MonoBehaviour {

    public Text timerText;
    public GameObject[] catPrefabs;

    public GameObject upperLeftSpawnLimit, upperRightSpawnLimit, lowerLeftSpawnLimit, lowerRightSpawnLimit;

    [SerializeField]
    int miniGameDuration = 20;
    [SerializeField]
    int numberOfCatsRunningAround = 10;
    Timer timer;

    List<GameObject> cats = new List<GameObject>();
    CatColor targetCatColor;

	void Start () {
        timer = new Timer();

        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;

        // Start the timer 
        timer.SetTimer(miniGameDuration);
        timerText.text = miniGameDuration.ToString();
        
    }

    private void Update()
    {
        // Dev controls
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartMiniGame();
        }

        // Dev controls
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetTargetCat(CatColor.Brown);
        }

    }

    void FixedUpdate()
    {
        // Update the timer
        timer.Update();
    }

    // Timer Callbacks

    void HandleTimeUp()
    {
        Debug.Log("Time is up!");
        StopMiniGame();
    }

    void HandleSecondsChanged(int secondsRemaining)
    {
        Debug.Log("Seconds Remaining: " + secondsRemaining);
        timerText.text = secondsRemaining.ToString();
    }

    // Mini Game Setup

    void BirthCats()
    {
        while (cats.Count < numberOfCatsRunningAround - 1)
        {
            int i = Random.Range(0, catPrefabs.Length);
            if (catPrefabs[i].GetComponent<Cat>().catColor == targetCatColor)
                continue;
            GameObject catObject = Instantiate(catPrefabs[i], GetSpawnPoint(), Quaternion.identity);
            cats.Add(catObject);
        }

        for (int i = 0; i < catPrefabs.Length; i++)
        {
            if (catPrefabs[i].GetComponent<Cat>().catColor == targetCatColor)
            {
                GameObject targetCatObject = Instantiate(catPrefabs[i], GetSpawnPoint(), Quaternion.identity);
                cats.Add(targetCatObject);
            }
                
        }
    }

    Vector3 GetSpawnPoint()
    {
        Vector3 position = Vector3.zero;
        bool left = Random.value > 0.5f;
        position.x = left ? lowerLeftSpawnLimit.transform.position.x : lowerRightSpawnLimit.transform.position.x;
        position.y = Random.Range(left ? lowerLeftSpawnLimit.transform.position.y : lowerRightSpawnLimit.transform.position.y, 
            left ? upperLeftSpawnLimit.transform.position.y : upperRightSpawnLimit.transform.position.y);
        
        return position;
    }

    // Mini Game Control

    public void StartMiniGame()
    {
        Debug.Log("Cat MiniGame Started");

        timer.StartTimer();
        StartCoroutine("RunGame");
    }

    public void StopMiniGame()
    {
        Debug.Log("Cat MiniGame Stopped");
        StopCoroutine("RunGame");
    }
    
    IEnumerator RunGame()
    {
        Debug.Log("Cat MiniGame Running");
        
        yield return null;

        // Spawn cats off screen to either side

        // Make cats run across to other side

        // Kill the cat? no just reuse it #save the cats

        RunGame();
    }

    public void SetTargetCat(CatColor targetCatColor)
    {
        Debug.Log("Setting targetCatColor to " + targetCatColor.ToString());
        this.targetCatColor = targetCatColor;
        BirthCats();
    }
}
