using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour {

    [SerializeField] private Transform cutLinePrefab;
    [SerializeField] private Transform cutItems;
    [SerializeField] private Transform cookPot;
    
    private int currCutPoint;
    private int currCutItem;
    private bool end_minigame;
    public float knifeSpeed = 0.01f;
    private Vector3 initialKnifePos;

    private int score;

    Timer timer;

    // Use this for initialization
    void Start ()
    {
        //initialize game object pointers
        currCutItem = 0;
        currCutPoint = 0;

        //Display 1st item
        cutItems.GetChild(currCutItem).gameObject.SetActive(true);

        score = 0;
        end_minigame = false;
        initialKnifePos = this.transform.position;

        // Create the timer
        timer = new Timer();
        // Register for the events
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;

        // Start the timer 
        timer.SetTimer(30);
        timer.StartTimer();
    }
	
    public int GetScore()
    {
        return score;
    }

    void HandleTimeUp()
    {
        //end game
        Debug.Log("Kinves Down!");
        //end_minigame = true;
        //GameManager.Instance.CookingScore += score;
        //LevelManager.Instance.LoadScene(Level.MainGame);
        //add transition to main game from minigame
    }

    void HandleSecondsChanged(int secondsRemaining)
    {
        //Debug.Log("Seconds Remaining: " + secondsRemaining);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        timer.Update();

        //Debug.Log(score);

        //Move knife across screen
        if (this.transform.position.x < 2)
        {
            this.transform.position += new Vector3(knifeSpeed, 0);
        }
        else
        {
            this.transform.position = initialKnifePos;
        }

        //Cut Item on mouse click, if there is an item on the cut board (i.e. !end_minigame)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            if (!end_minigame)
            {
                // Check if knife is over item
                if (transform.position.x > cutItems.GetChild(currCutItem).GetComponent<SpriteRenderer>().bounds.min.x && transform.position.x < cutItems.GetChild(currCutItem).GetComponent<SpriteRenderer>().bounds.max.x)
                {
                    //increment score
                    score += 1;

                    //Create new cut line
                    Transform newCut = Instantiate(cutLinePrefab);
                    newCut.position = transform.position;
                    //Make it a child of the item
                    newCut.parent = cutItems.GetChild(currCutItem).GetChild(1);
                    //Get cut points
                    Transform cutPoints = cutItems.GetChild(currCutItem).GetChild(0);

                    //Child 0 should be the cut points
                    cutItems.GetChild(currCutItem).GetChild(0).GetChild(currCutPoint).gameObject.SetActive(false);

                    if (newCut.position.x >= cutPoints.GetChild(currCutPoint).position.x - 0.02f && newCut.position.x <= cutPoints.GetChild(currCutPoint).position.x + 0.02f)
                    {
                        //increment score for accuracy
                        score += 1;
                    }

                    //next cut point
                    currCutPoint++;

                    //check if item fully sliced
                    if (currCutPoint == cutPoints.childCount)
                    {
                        //Hide chopped up items
                        cutItems.GetChild(currCutItem).gameObject.SetActive(false);

                        cutItems.GetChild(currCutItem).GetChild(2).parent = cookPot;
                        cookPot.GetChild(currCutItem).localPosition = new Vector3(0, 0, 0);
                        //show diced items in pot
                        cookPot.GetChild(currCutItem).gameObject.SetActive(true);

                        //next item
                        currCutItem++;

                        //check if last item
                        if (currCutItem == cutItems.childCount)
                        {
                            //end game
                            end_minigame = true;
                            GameManager.Instance.CookingScore += score;
                            if(score > 30)
                            {
                                GameManager.Instance.CurrentNPCScript.ChangeMentalState((int)GameManager.Kindness.Best);
                            }
                            GameManager.Instance.CurrentNPCScript.ChangeMentalState((int)GameManager.Kindness.Good);

                            Debug.Log(GameManager.Instance.CookingScore);
                            GameManager.Instance.playedJon = true;
                            LevelManager.Instance.LoadScene(Level.MainGame);
                            //add transition to main game from minigame
                        }
                        else
                        {
                            //show next item to be cut
                            cutItems.GetChild(currCutItem).gameObject.SetActive(true);
                            currCutPoint = 0;
                        }
                    }
                }
            }
        }
	}
    
}
