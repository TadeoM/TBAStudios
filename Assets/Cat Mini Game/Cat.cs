using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    public CatColor catColor;
    public float speed = 5f;

    public bool running;

    public delegate void CatSelected(Cat catColor);
    public event CatSelected OnCatSelected;

    public AudioClip[] dissatisfiedSounds;
    public AudioClip[] satisfiedSounds;

    enum CatSpeed { Slow = 1, Normal = 2, Fast = 3, Max}
    CatSpeed speedMod = CatSpeed.Normal;
    Vector3 targetPosition;
    AudioSource audioSource;
    bool facingRight;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (running)
        {
            // Debug.Log(name + " running");
            transform.position  = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed * (int)speedMod);
            if (transform.position == targetPosition)
            {
                running = false;
                // Debug.Log(name + " stopped running");
            }
        }
	}

    public void StartRunning()
    {
        targetPosition = new Vector3(-transform.position.x, -transform.position.y, transform.position.z);
        speedMod = (CatSpeed)Mathf.FloorToInt(Random.Range(1, 3));
        facingRight = transform.position.x < 0;
        transform.localScale = new Vector3(facingRight ? -1 : 1, transform.localScale.y, transform.localScale.z); 
        running = true;
    }

    public void StopRunning()
    {
        running = false;
    }

    public void MaxSpeed()
    {
        speedMod = CatSpeed.Max;
    }

    private void OnMouseDown()
    {
        // Debug.Log("You hit " + name + "!! how could you!");
        if (OnCatSelected != null)
            OnCatSelected(this);
    }

    public void PlaySatisfied()
    {
        //audioSource.PlayOneShot(satisfiedSounds[Random.Range(0,satisfiedSounds.Length-1)]);
    }

    public void PlayDissatisfied()
    {
        //audioSource.PlayOneShot(dissatisfiedSounds[Random.Range(0, dissatisfiedSounds.Length-1)]);

    }
}
