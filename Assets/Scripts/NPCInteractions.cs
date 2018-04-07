using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractions : MonoBehaviour {
    private bool isTriggered = false;
    private string currentMinigame;
    public string[] possibleMinigames;
    private float happiness;
    private SpriteRenderer silhouette;
    private Transform child;

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
    void Awake() {
        NewMiniGame();
        happiness = 100;
        child = transform.GetChild(0);
        silhouette = child.GetComponent<SpriteRenderer>();
        ChangeMentalState(0);
    }
	
	// Update is called once per frame
	void Update() {
        ChangeMentalState(-1f);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
    }
    public void NewMiniGame()
    {
        currentMinigame = possibleMinigames[Random.Range(0, possibleMinigames.Length)];
    }

    public void ChangeMentalState(float hapVal)
    {
        happiness += hapVal;
        if(happiness < 0)
        {
            happiness = 0;
            
        }
        silhouette.color = new Color(silhouette.color.r, silhouette.color.g, silhouette.color.b, happiness / 100);
    }
}
