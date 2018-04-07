using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoarderHand : MonoBehaviour {

    private int itemsToSteal;
    private OrganizeBox target;
    private Vector3 placehold1;
    private Vector3 placehold2;

    public OrganizeBox Target
    {
        get { return target; }
        set { target = value; }
    }

	// Use this for initialization
	void Start () {
        placehold1 = GameObject.FindGameObjectWithTag("Placeholder1").GetComponent<Transform>().position;
        placehold2 = GameObject.FindGameObjectWithTag("Placeholder2").GetComponent<Transform>().position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 oldPosition = GetComponent<Transform>().position;
        oldPosition.x = target.GetComponent<Transform>().position.x;
        if(target.Position.y == 0)
        {
            oldPosition.y = placehold1.y;
        }
        else
        {
            oldPosition.y = placehold2.y;
        }
        GetComponent<Transform>().position = oldPosition;
	}
}
