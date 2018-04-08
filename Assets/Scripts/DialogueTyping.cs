using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTyping : MonoBehaviour {
    //attributes
    string wordSun;
    string wordStar;
    string wordBonfire;
    string wordCandle;

    int charPosSun;
    int charPosStar;
    int charPosBonfire;
    int charPosCandle;

    public Canvas canvasParent;

    public Text charBlackSun;
    public Text charWhiteSun;

    public Text charBlackStar;
    public Text charWhiteStar;

    public Text charBlackBonfire;
    public Text charWhiteBonfire;

    public Text charBlackCandle;
    public Text charWhiteCandle;

    public Text[] poem = new Text[3];

    float spacing;

    Vector3 posBlackSun;
    Vector3 posWhiteSun;

    Vector3 posBlackStar;
    Vector3 posWhiteStar;

    Vector3 posBlackBonfire;
    Vector3 posWhiteBonfire;

    Vector3 posBlackCandle;
    Vector3 posWhiteCandle;

    List<Text> charsBlackSun;
    List<Text> charsWhiteSun;

    List<Text> charsBlackStar;
    List<Text> charsWhiteStar;

    List<Text> charsBlackBonfire;
    List<Text> charsWhiteBonfire;

    List<Text> charsBlackCandle;
    List<Text> charsWhiteCandle;

    string[] linesSun;
    string[] linesStar;
    string[] linesBonfire;
    string[] linesCandle;

    int line;

    bool finish;

    int sunCompletion = 0;
    int starCompletion = 0;
    int bonfireCompletion = 0;
    int candleCompletion = 0;

    // Use this for initialization
    void Start() {
        
        linesSun = System.IO.File.ReadAllLines(@"Text\Sun.txt");
        linesStar = System.IO.File.ReadAllLines(@"Text\Star.txt");
        linesBonfire = System.IO.File.ReadAllLines(@"Text\Bonfire.txt");
        linesCandle = System.IO.File.ReadAllLines(@"Text\Candle.txt");

        spacing = 25;

        charPosSun = 0;
        charPosStar = 0;
        charPosBonfire = 0;
        charPosCandle = 0;

        line = 0;

        wordSun = linesSun[line];
        wordStar = linesStar[line];
        wordBonfire = linesBonfire[line];
        wordCandle = linesCandle[line];

        charsBlackSun = new List<Text>();
        charsWhiteSun = new List<Text>();

        charsBlackStar = new List<Text>();
        charsWhiteStar = new List<Text>();

        charsBlackBonfire = new List<Text>();
        charsWhiteBonfire = new List<Text>();

        charsBlackCandle = new List<Text>();
        charsWhiteCandle = new List<Text>();

        /*
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
        */
        SetUp();

        finish = true;
    }
	
	// Update is called once per frame
	void Update () {
        /*
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

            poem[line].text = lines[line];

            line++;

            if(line < lines.Length)
            {
                CharArrayReset();
            }
        }
        */
        GamePlay();

        if (poem[2].text != "" && finish)
        {
            for (int i = 0; i < charsWhiteSun.Count; i++)
            {
                Destroy(charsWhiteSun[i]);
            }

            for (int i = 0; i < charsBlackSun.Count; i++)
            {
                Destroy(charsBlackSun[i]);
            }

            for (int i = 0; i < charsWhiteStar.Count; i++)
            {
                Destroy(charsWhiteStar[i]);
            }

            for (int i = 0; i < charsBlackStar.Count; i++)
            {
                Destroy(charsBlackStar[i]);
            }

            for (int i = 0; i < charsWhiteBonfire.Count; i++)
            {
                Destroy(charsWhiteBonfire[i]);
            }

            for (int i = 0; i < charsBlackBonfire.Count; i++)
            {
                Destroy(charsBlackBonfire[i]);
            }

            for (int i = 0; i < charsWhiteCandle.Count; i++)
            {
                Destroy(charsWhiteCandle[i]);
            }

            for (int i = 0; i < charsBlackCandle.Count; i++)
            {
                Destroy(charsBlackCandle[i]);
            }

            if(sunCompletion == 3)
            {
                Debug.Log("Completed Sun");
            }

            if (starCompletion == 3)
            {
                Debug.Log("Completed Star");
            }

            if (bonfireCompletion == 3)
            {
                Debug.Log("Completed Bonfire");
            }

            if (candleCompletion == 3)
            {
                Debug.Log("Completed Candle");
            }

            finish = false;
        }
    }

    void GamePlay()
    {
        if (charPosSun < wordSun.Length)
        {
            if (wordSun.Substring(charPosSun, 1) == " ")
            {
                charPosSun++;
            }
            else if (Input.GetKeyDown(wordSun.Substring(charPosSun, 1)))
            {
                charsWhiteSun[charPosSun].text = wordSun.Substring(charPosSun, 1);
                Destroy(charsBlackSun[charPosSun]);
                charPosSun++;
            }
        }

        if (charPosSun == wordSun.Length)
        {
            charPosSun = 0;

            poem[line].text = linesSun[line];

            line++;
            sunCompletion++;

            if (line < linesSun.Length)
            {
                CharArrayReset();
            }
        }

        //-------------------------------------------------

        if (charPosStar < wordStar.Length)
        {
            if (wordStar.Substring(charPosStar, 1) == " ")
            {
                charPosStar++;
            }
            else if (Input.GetKeyDown(wordStar.Substring(charPosStar, 1)))
            {
                charsWhiteStar[charPosStar].text = wordStar.Substring(charPosStar, 1);
                Destroy(charsBlackStar[charPosStar]);
                charPosStar++;
            }
        }

        if (charPosStar == wordStar.Length)
        {
            charPosStar = 0;

            poem[line].text = linesStar[line];

            line++;
            starCompletion++;

            if (line < linesStar.Length)
            {
                CharArrayReset();
            }
        }

        //----------------------------------------------------------
        if (charPosBonfire < wordBonfire.Length)
        {
            if (wordBonfire.Substring(charPosBonfire, 1) == " ")
            {
                charPosBonfire++;
            }
            else if (Input.GetKeyDown(wordBonfire.Substring(charPosBonfire, 1)))
            {
                charsWhiteBonfire[charPosBonfire].text = wordBonfire.Substring(charPosBonfire, 1);
                Destroy(charsBlackBonfire[charPosBonfire]);
                charPosBonfire++;
            }
        }

        if (charPosBonfire == wordBonfire.Length)
        {
            charPosBonfire = 0;

            poem[line].text = linesBonfire[line];

            line++;
            bonfireCompletion++;

            if (line < linesBonfire.Length)
            {
                CharArrayReset();
            }
        }

        //--------------------------------------------------------

        if (charPosCandle < wordCandle.Length)
        {
            if (wordCandle.Substring(charPosCandle, 1) == " ")
            {
                charPosCandle++;
            }
            else if (Input.GetKeyDown(wordCandle.Substring(charPosCandle, 1)))
            {
                charsWhiteCandle[charPosCandle].text = wordCandle.Substring(charPosCandle, 1);
                Destroy(charsBlackCandle[charPosCandle]);
                charPosCandle++;
            }
        }

        if (charPosCandle == wordCandle.Length)
        {
            charPosCandle = 0;

            poem[line].text = linesCandle[line];

            line++;
            candleCompletion++;

            if (line < linesCandle.Length)
            {
                CharArrayReset();
            }
        }
    }

    void CharArrayReset()
    {
        /*
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
        */

        charPosSun = 0;
        charPosStar = 0;
        charPosBonfire = 0;
        charPosCandle = 0;

        Vector3[] charBlackWhite = new Vector3[8];
        charBlackWhite[0] = charBlackSun.transform.position;
        charBlackWhite[1] = charWhiteSun.transform.position;
        charBlackWhite[2] = charBlackStar.transform.position;
        charBlackWhite[3] = charWhiteStar.transform.position;
        charBlackWhite[4] = charBlackBonfire.transform.position;
        charBlackWhite[5] = charWhiteBonfire.transform.position;
        charBlackWhite[6] = charBlackCandle.transform.position;
        charBlackWhite[7] = charWhiteCandle.transform.position;

        int[] newPos = new int[4];

        newPos[0] = Random.Range(0, 4);
        newPos[1] = Random.Range(0,4);
        newPos[2] = Random.Range(0,4);
        newPos[3] = Random.Range(0,4);

        while(newPos[1] == newPos[0])
        {
            newPos[1] = Random.Range(0, 4);
        }

        while(newPos[2] == newPos[1] || newPos[2] == newPos[0])
        {
            newPos[2] = Random.Range(0, 4);
        }

        while(newPos[3] == newPos[2] || newPos[3] == newPos[1] || newPos[3] == newPos[0])
        {
            newPos[3] = Random.Range(0, 4);

        }

        for (int i = 0; i < 4; i++)
        {
            newPos[i] = newPos[i] * 2;
            Debug.Log(newPos[i]);
        }

        posBlackSun = charBlackWhite[newPos[0]];
        posWhiteSun = charBlackWhite[newPos[0] + 1];

        posBlackStar = charBlackWhite[newPos[1]];
        posWhiteStar = charBlackWhite[newPos[1] + 1];

        posBlackBonfire = charBlackWhite[newPos[2]];
        posWhiteBonfire = charBlackWhite[newPos[2] + 1];

        posBlackCandle = charBlackWhite[newPos[3]];
        posWhiteCandle = charBlackWhite[newPos[3] + 1];

        wordSun = linesSun[line];

        for (int i = 0; i < charsWhiteSun.Count; i++)
        {
            Destroy(charsWhiteSun[i]);
        }

        for (int i = 0; i < charsBlackSun.Count; i++)
        {
            Destroy(charsBlackSun[i]);
        }

        charsBlackSun = new List<Text>();
        charsWhiteSun = new List<Text>();

        for (int i = 0; i < wordSun.Length; i++)
        {
            posBlackSun.x += spacing;
            charsBlackSun.Add(Instantiate(charBlackSun, posBlackSun, Quaternion.identity));
            charsBlackSun[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordSun.Length; i++)
        {
            posWhiteSun.x += spacing;
            charsWhiteSun.Add(Instantiate(charWhiteSun, posWhiteSun, Quaternion.identity));
            charsWhiteSun[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordSun.Length; i++)
        {
            charsBlackSun[i].text = wordSun.Substring(i, 1);
        }

        //---------------------------------------------------------

        wordStar = linesStar[line];

        for (int i = 0; i < charsWhiteStar.Count; i++)
        {
            Destroy(charsWhiteStar[i]);
        }

        for (int i = 0; i < charsBlackStar.Count; i++)
        {
            Destroy(charsBlackStar[i]);
        }

        charsBlackStar = new List<Text>();
        charsWhiteStar = new List<Text>();

        for (int i = 0; i < wordStar.Length; i++)
        {
            posBlackStar.x += spacing;
            charsBlackStar.Add(Instantiate(charBlackStar, posBlackStar, Quaternion.identity));
            charsBlackStar[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordStar.Length; i++)
        {
            posWhiteStar.x += spacing;
            charsWhiteStar.Add(Instantiate(charWhiteStar, posWhiteStar, Quaternion.identity));
            charsWhiteStar[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordStar.Length; i++)
        {
            charsBlackStar[i].text = wordStar.Substring(i, 1);
        }

        //--------------------------------------------------------

        wordBonfire = linesBonfire[line];

        for (int i = 0; i < charsWhiteBonfire.Count; i++)
        {
            Destroy(charsWhiteBonfire[i]);
        }

        for (int i = 0; i < charsBlackBonfire.Count; i++)
        {
            Destroy(charsBlackBonfire[i]);
        }

        charsBlackBonfire = new List<Text>();
        charsWhiteBonfire = new List<Text>();

        for (int i = 0; i < wordBonfire.Length; i++)
        {
            posBlackBonfire.x += spacing;
            charsBlackBonfire.Add(Instantiate(charBlackBonfire, posBlackBonfire, Quaternion.identity));
            charsBlackBonfire[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordBonfire.Length; i++)
        {
            posWhiteBonfire.x += spacing;
            charsWhiteBonfire.Add(Instantiate(charWhiteBonfire, posWhiteBonfire, Quaternion.identity));
            charsWhiteBonfire[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordBonfire.Length; i++)
        {
            charsBlackBonfire[i].text = wordBonfire.Substring(i, 1);
        }

        //------------------------------------------------------

        wordCandle = linesCandle[line];

        for (int i = 0; i < charsWhiteCandle.Count; i++)
        {
            Destroy(charsWhiteCandle[i]);
        }

        for (int i = 0; i < charsBlackCandle.Count; i++)
        {
            Destroy(charsBlackCandle[i]);
        }

        charsBlackCandle = new List<Text>();
        charsWhiteCandle = new List<Text>();

        for (int i = 0; i < wordCandle.Length; i++)
        {
            posBlackCandle.x += spacing;
            charsBlackCandle.Add(Instantiate(charBlackCandle, posBlackCandle, Quaternion.identity));
            charsBlackCandle[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordCandle.Length; i++)
        {
            posWhiteCandle.x += spacing;
            charsWhiteCandle.Add(Instantiate(charWhiteCandle, posWhiteCandle, Quaternion.identity));
            charsWhiteCandle[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordCandle.Length; i++)
        {
            charsBlackCandle[i].text = wordCandle.Substring(i, 1);
        }
    }

    void SetUp()
    {
        posBlackSun = charBlackSun.transform.position;
        posWhiteSun = charWhiteSun.transform.position;

        for (int i = 0; i < wordSun.Length; i++)
        {
            posBlackSun.x += spacing;
            charsBlackSun.Add(Instantiate(charBlackSun, posBlackSun, Quaternion.identity));
            charsBlackSun[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordSun.Length; i++)
        {
            posWhiteSun.x += spacing;
            charsWhiteSun.Add(Instantiate(charWhiteSun, posWhiteSun, Quaternion.identity));
            charsWhiteSun[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordSun.Length; i++)
        {
            charsBlackSun[i].text = wordSun.Substring(i, 1);
        }

        //------------------------

        posBlackStar = charBlackStar.transform.position;
        posWhiteStar = charWhiteStar.transform.position;

        for (int i = 0; i < wordStar.Length; i++)
        {
            posBlackStar.x += spacing;
            charsBlackStar.Add(Instantiate(charBlackStar, posBlackStar, Quaternion.identity));
            charsBlackStar[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordStar.Length; i++)
        {
            posWhiteStar.x += spacing;
            charsWhiteStar.Add(Instantiate(charWhiteStar, posWhiteStar, Quaternion.identity));
            charsWhiteStar[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordStar.Length; i++)
        {
            charsBlackStar[i].text = wordStar.Substring(i, 1);
        }

        //------------------------------

        posBlackBonfire = charBlackBonfire.transform.position;
        posWhiteBonfire = charWhiteBonfire.transform.position;

        for (int i = 0; i < wordBonfire.Length; i++)
        {
            posBlackBonfire.x += spacing;
            charsBlackBonfire.Add(Instantiate(charBlackBonfire, posBlackBonfire, Quaternion.identity));
            charsBlackBonfire[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordBonfire.Length; i++)
        {
            posWhiteBonfire.x += spacing;
            charsWhiteBonfire.Add(Instantiate(charWhiteBonfire, posWhiteBonfire, Quaternion.identity));
            charsWhiteBonfire[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordBonfire.Length; i++)
        {
            charsBlackBonfire[i].text = wordBonfire.Substring(i, 1);
        }

        //----------------------------

        posBlackCandle = charBlackCandle.transform.position;
        posWhiteCandle = charWhiteCandle.transform.position;

        for (int i = 0; i < wordCandle.Length; i++)
        {
            posBlackCandle.x += spacing;
            charsBlackCandle.Add(Instantiate(charBlackCandle, posBlackCandle, Quaternion.identity));
            charsBlackCandle[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordCandle.Length; i++)
        {
            posWhiteCandle.x += spacing;
            charsWhiteCandle.Add(Instantiate(charWhiteCandle, posWhiteCandle, Quaternion.identity));
            charsWhiteCandle[i].transform.SetParent(canvasParent.transform);
        }

        for (int i = 0; i < wordCandle.Length; i++)
        {
            charsBlackCandle[i].text = wordCandle.Substring(i, 1);
        }
    }
}