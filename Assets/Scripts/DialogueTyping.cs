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

    public Text poemLine1;
    public Text poemLine2;
    public Text poemLine3;

    float spacing;

    Vector3 posBlack;
    Vector3 posWhite;

    List<Text> charsBlack;
    List<Text> charsWhite;

    string[] lines;

    int line;

    // Use this for initialization
    void Start() {

        lines = System.IO.File.ReadAllLines(@"C:\Users\Jeb\Desktop\School\Jams\IThrive Kindness Text\Sun.txt");

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
    }
	
	// Update is called once per frame
	void Update () {

        if (charPos < word.Length)
        {
            if(word.Substring(charPos, 1) == " ")
            {
                charPos++;
            }
            else if (Input.GetKeyDown(word.Substring(charPos, 1)))
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