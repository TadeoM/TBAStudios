using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTyping : MonoBehaviour {
    //attributes
    string word;
    int charPos;

    public Canvas canvasParent;

    public Text charBlack;
    public Text charWhite;

    float spacing;

    Vector3 posBlack;
    Vector3 posWhite;

    List<Text> charsBlack;
    List<Text> charsWhite;

    List<string[]> dialogueFiles = new List<string[]>();

    int dialogueFile;

    string[] lines;

    int line;

    public Text npcText;

    string[] npcLines = System.IO.File.ReadAllLines(@"C:\Users\Jeb\Desktop\School\Jams\IThrive Kindness Text\npcTest.txt");

    int npcLine;

    // Use this for initialization
    void Start() {

        dialogueFiles.Add(System.IO.File.ReadAllLines(@"C:\Users\Jeb\Desktop\School\Jams\IThrive Kindness Text\Greeting1.txt"));
        dialogueFiles.Add(System.IO.File.ReadAllLines(@"C:\Users\Jeb\Desktop\School\Jams\IThrive Kindness Text\Goodbye1.txt"));

        dialogueFile = 0;

        lines = dialogueFiles[dialogueFile];

        line = 0;

        spacing = 25;

        charPos = 0;

        word = lines[line];
        
        charsBlack = new List<Text>();
        charsWhite = new List<Text>();

        posBlack = charBlack.transform.position;
        posWhite = charWhite.transform.position;

        for (int i = 0; i < word.Length; i++)
        {
            posBlack.x += spacing;
            charsBlack.Add(Instantiate(charBlack, posBlack, Quaternion.identity));
            charsBlack[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < word.Length; i++)
        {
            posWhite.x += spacing;
            charsWhite.Add(Instantiate(charWhite, posWhite, Quaternion.identity));
            charsWhite[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < word.Length; i++)
        {
            charsBlack[i].text = word.Substring(i, 1);
        }

        npcLine = 0;

        npcText.text = npcLines[npcLine];
    }
	
	// Update is called once per frame
	void Update () {

        if (charPos < word.Length)
        {
            if (Input.GetKeyDown(word.Substring(charPos, 1)))
            {
                charsWhite[charPos].text = word.Substring(charPos, 1);
                Destroy(charsBlack[charPos]);
                charPos++;
            }
        }

        if (charPos == word.Length)
        {
            charPos = 0;
            line++;

            if(line < lines.Length)
            {
                CharArrayReset();
            }
        }

        if(line == lines.Length)
        {
            line = 0;

            dialogueFile++;

            if (dialogueFile < dialogueFiles.Count)
            {
                lines = dialogueFiles[dialogueFile];
            }

            if(dialogueFile != dialogueFiles.Count)
            {
                CharArrayReset();
            }
            npcLine++;

            if(npcLine < npcLines.Length)
            {
                npcText.text = npcLines[npcLine];
            }
        }
    }

    void CharArrayReset()
    {
        word = lines[line];

        for (int i = 0; i < charsWhite.Count; i++)
        {
            Destroy(charsWhite[i]);
        }

        charsBlack = new List<Text>();
        charsWhite = new List<Text>();

        posBlack = charBlack.transform.position;
        posWhite = charWhite.transform.position;

        for (int i = 0; i < word.Length; i++)
        {
            posBlack.x += spacing;
            charsBlack.Add(Instantiate(charBlack, posBlack, Quaternion.identity));
            charsBlack[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < word.Length; i++)
        {
            posWhite.x += spacing;
            charsWhite.Add(Instantiate(charWhite, posWhite, Quaternion.identity));
            charsWhite[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < word.Length; i++)
        {
            charsBlack[i].text = word.Substring(i, 1);
        }
    }
}