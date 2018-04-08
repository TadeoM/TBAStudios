using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {
    private float happiness;
    private float energy;
    private SpriteRenderer silhouette;
    private Transform child;

    public float Energy
    {
        get { return energy; }
        set { energy = value; }
    }


    public float Happiness
    {
        get { return happiness; }
        set { happiness = value; }
    }

    // Use this for initialization
    void Start () {
        happiness = 100;
        energy = 100;
        child = transform.GetChild(0);
        silhouette = child.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        ChangeMentalStates(-1, -1);
    }

    public void ChangeMentalStates(float hapVal, float engVal)
    {
        happiness += hapVal;
        energy += engVal;

        if (happiness < 0)
        {
            happiness = 0;
        }
        if(energy < 0)
        {
            energy = 0;
        }
        silhouette.color = new Color(silhouette.color.r, silhouette.color.g, silhouette.color.b, happiness / 100);
    }
}
