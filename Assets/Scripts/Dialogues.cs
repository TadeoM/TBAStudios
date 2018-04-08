using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour {

    public GameObject joeJones;
    public GameObject elizabeth;
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
        SetupElizabeth();
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

    public void SetupElizabeth()
    {
        string[,] conversation = elizabeth.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[1, 8] 
        { 
            { "'Scuse me young man, have you seen my cat, My.Snugglesworth",
                "Sorry, I haven't seen him/No, but what does it look like?",
                "Oh Mr.Snugglesworth, where are you....",
                "HE is light grey with grey feet and tail",
                "Why did he run away?/I'll track him down!",
                "Well he hasn't eaten in a dayy...I forgot to feed him/Oh! Thank you so much!",
                "I'll help you find him, buut you can't stop feeding him/Maybe if you bring out food he'll come back.",
                "You're right I'll be more particular next time. Thank you/Thank you, you're a doll!" }
        } ;
        elizabeth.GetComponent<NPCInteractions>().Conversations = conversation;
        elizabeth.GetComponent<NPCInteractions>().NewDaySetup();
    }

    public void SetupWilliam()
    {
        string[,] conversation = william.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[1, 7]{
            { "*munch munch*", "Wow./You should probably cook that before you eat it.",
                ".../I don't know how to cook.",
                "That is quite unhealthy, you should learn some recipes/I'm going to cook some food for us.",
                "I don't have the time/Oh that sounds incredible.",
                "Me too bud, but cooking your own food will give you energy/Let's cook some food together and you'll learn the recipe",
                "I guess you're right, I'll try to learn some recipes/That sounds awesome! Let's do it." }
        };
        william.GetComponent<NPCInteractions>().Conversations = conversation;
        william.GetComponent<NPCInteractions>().NewDaySetup();
    }
    public void SetupSpongegar()
    {
        string[,] conversation = spongegar.GetComponent<NPCInteractions>().Conversations;
        conversation = new string[1, 7]{
            { "...", ".../So..what are you doing?",
                ".../I'm trying to think of what my next art piece should be.",
                "Is there any way I can help?/I can give you some ideas",
                "I usually have poems to help me inspire me/That might help.",
                "I'm confident we can get some ideas/I've been told my poetry is really good",
                "I'll put my faith in you/I hope you're right" },
        };
        spongegar.GetComponent<NPCInteractions>().Conversations = conversation;
        spongegar.GetComponent<NPCInteractions>().NewDaySetup();
    }
}
