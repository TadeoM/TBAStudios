using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    public CatColor catColor;
    public float speed = 2f;

    public bool running;

    public delegate void CatSelected(Cat catColor);
    public event CatSelected OnCatSelected;

    enum CatSpeed { Slow = 1, Normal = 2, Fast = 3, Max}
    CatSpeed speedMod = CatSpeed.Normal;
    Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            Debug.Log(name + " running");
            transform.position  = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed * (int)speedMod);
            if (transform.position == targetPosition)
            {
                running = false;
                Debug.Log(name + " stopped running");
            }
        }
	}

    public void StartRunning()
    {
        targetPosition = new Vector3(-transform.position.x, -transform.position.y, transform.position.z);
        speedMod = (CatSpeed)Mathf.FloorToInt(Random.Range(1, 3));
        running = true;
    }

    public void StopRunning()
    {
        running = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("You hit " + name + "!! how could you!");
        if (OnCatSelected != null)
            OnCatSelected(this);
    }
}
