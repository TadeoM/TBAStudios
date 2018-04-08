using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuController : MonoBehaviour {

    public TextMeshProUGUI title;
    public TextMeshProUGUI playTextButton;
    Camera currentCamera;

    // Use this for initialization
    void Start () {
        currentCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        // Dev Controls
        if (Input.GetKeyDown(KeyCode.X))
        {
            BackToMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ZoomToPlayer();
        }
    }

    public void PlayGame()
    {
        Debug.Log("Play game button pressed");

        Sequence mainMenuSequence = DOTween.Sequence();

        mainMenuSequence
            .Append(playTextButton.DOFade(0, 1f))
            .Join(title.DOFade(0, 3))
            .Join(currentCamera.transform.DOMoveY(-7.2f, 5))

            ;
        
        
        

        mainMenuSequence.Play();

    }

    public void ZoomToPlayer()
    {
        DOTween.To(orthoSize => currentCamera.orthographicSize = orthoSize, 5.82f, 1.3f, 2);
        GameObject.FindGameObjectWithTag("player");
        //transform.DOMove(GameObject.FindGameObjectWithTag("player").transform.Find("Camera Target").transform.position, 2);
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to main menu pressed");

        Sequence backToMainMenuSequence = DOTween.Sequence();

        backToMainMenuSequence
            .Append(currentCamera.transform.DOMoveY(4.87f, 5))
            
            .Join(title.DOFade(1, 3))
            .Append(playTextButton.DOFade(1, 1f))
            ;

        backToMainMenuSequence.Play();


    }
}
