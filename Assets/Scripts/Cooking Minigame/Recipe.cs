using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    [SerializeField] private Transform cutItems;
    [SerializeField] private GameObject knife;
    [SerializeField] private int numItems;

    Timer timer;

    public List<string> itemsSelected = new List<string>();

    // Use this for initialization
    void Start()
    {
        // Create the timer
        timer = new Timer();
        // Register for the events
        timer.OnTimeUp += HandleTimeUp;
        timer.OnSecondsChanged += HandleSecondsChanged;

        // Start the timer 
        timer.SetTimer(2);
        timer.StartTimer();
    }
    void HandleTimeUp()
    {
        Debug.Log("Time is up!");
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void HandleSecondsChanged(int secondsRemaining)
    {
        //Debug.Log("Seconds Remaining: " + secondsRemaining);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer.Update();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if(hitCollider.tag == "Item")
            {
                hitCollider.transform.parent = cutItems;
                hitCollider.GetComponent<SpriteRenderer>().sortingOrder = 1;
                hitCollider.transform.position = new Vector3(0, 0, 0);
                hitCollider.gameObject.SetActive(false);
                numItems--;
                if(numItems == 0)
                {
                    this.gameObject.SetActive(false);
                    //set active transition screen instead of knife
                    knife.SetActive(true);
                }
            }

            if (hitCollider.tag == "TransitionScreen")
            {
                hitCollider.gameObject.SetActive(false);
                knife.SetActive(true);
            }
        }
    }
}
