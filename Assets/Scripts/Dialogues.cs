using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour {

    public GameObject joeJones;
    public GameObject sarah;
    public GameObject william;
    public GameObject spongegar;
    public bool isSetup;

	// Use this for initialization
	void Start ()
    {
        isSetup = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupEverything()
    {
        SetupJoeJones();
        SetupSarah();
        SetupWilliam();
        SetupSpongegar();
        isSetup = true;
    }

    public void SetupJoeJones()
    {
        string[,] conversation = joeJones.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[1, 7]{
            { "Hey...have you seen my Super Beaver Man Volume 2...",
                "No/Well maybe it's lost in your house?", 
                "Okay bye/What do you mean?", /*Okay bye ends dialogue*/
                "Explain in Detail.../I'll organize for you and you'll see",
                "You're right/Thanks", /*Thanks ends dialogue and starts minigame*/
                "I can help if you'd like/Okay, good luck!", 
                "Okay that makes sense, let's do this/Thanks, I appreciate it!" /*okay, let's do this ends dialogue and starts minigame */}
        };
        joeJones.GetComponent<NPCInteractions>().Conversations = conversation;
        Debug.Log(joeJones.GetComponent<NPCInteractions>().Conversations.Length);
        joeJones.GetComponent<NPCInteractions>().NewDaySetup();
    }

    public void SetupSarah()
    {
        string[,] conversation = sarah.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[5, 5] 
        { 
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" }
        } ;
        sarah.GetComponent<NPCInteractions>().Conversations = conversation;
        sarah.GetComponent<NPCInteractions>().NewDaySetup();
    }

    public void SetupWilliam()
    {
        string[,] conversation = william.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[5, 5]{
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" }
        };
        william.GetComponent<NPCInteractions>().Conversations = conversation;
        william.GetComponent<NPCInteractions>().NewDaySetup();
    }
    public void SetupSpongegar()
    {
        string[,] conversation = spongegar.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[5, 5]{
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" },
            { "one", "Two", "Three", "Four", "Five" }
        };
        spongegar.GetComponent<NPCInteractions>().Conversations = conversation;
        spongegar.GetComponent<NPCInteractions>().NewDaySetup();
    }
}
