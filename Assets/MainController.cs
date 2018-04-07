using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] nonPlayers;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        FindNonPlayers();
    } 
	
	// Update is called once per frame
	void FixedUpdate () {
        
    }

    /// <summary>
    /// Find the NPC characters
    /// </summary>
    void FindNonPlayers()
    {
        nonPlayers = GameObject.FindGameObjectsWithTag("NPC");
    }
}
