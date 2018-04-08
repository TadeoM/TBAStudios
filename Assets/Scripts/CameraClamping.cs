using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamping : MonoBehaviour {

    public Vector2 minBounds; // -9, -6.425
    public Vector2 maxBounds; // 9 , 6.425

    public float bottomOfApartment = -7.8f;
    public float apartmentWidth = 11.5f;
    // -7.8f bottom of the apartment

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 position = transform.position;

        float orthoWidth = Camera.main.orthographicSize * Screen.width / Screen.height;

        position.x = Mathf.Clamp(position.x, -apartmentWidth + orthoWidth, apartmentWidth - orthoWidth);
        position.y = Mathf.Clamp(position.y, Camera.main.orthographicSize + bottomOfApartment, 10);

        transform.position = position;
    }
}
