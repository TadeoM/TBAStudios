using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextJiggle : MonoBehaviour {

    bool vertMove;
    bool horizMove;

    int vertTime;
    int horizTime;

    int randScale;
    int scale;

    int randVert;
    int randHoriz;

	// Use this for initialization
	void Start () {
        vertMove = true;
        horizMove = true;

        vertTime = 0;
        horizTime = 0;

        int randScale = Random.Range(0, 1);

        if(randScale == 0)
        {
            scale = 4;
        }
        else
        {
            scale = -4;
        }

        randVert = Random.Range(15, 45);
        randHoriz = Random.Range(30, 60);
    }
	
	// Update is called once per frame
	void Update () {
        if (vertTime > randVert)
        {
            if (vertMove)
            {
                transform.Translate(Vector3.up * scale);
                vertMove = !vertMove;
            }
            else
            {
                transform.Translate(Vector3.down * scale);
                vertMove = !vertMove;
            }
            randVert = Random.Range(15, 45);
            vertTime = 0;
        }

        if(horizTime > randHoriz)
        {
            if (horizMove)
            {
                transform.Translate(Vector3.right * scale);
                horizMove = !horizMove;
            }
            else
            {
                transform.Translate(Vector3.left * scale);
                horizMove = !horizMove;
            }
            randHoriz = Random.Range(30, 60);
            horizTime = 0;
        }

        vertTime++;
        horizTime++;
    }
}
