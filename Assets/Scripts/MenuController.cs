using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityStandardAssets._2D;

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
            ZoomOutPlayer();
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
            .Join(currentCamera.transform.DOMoveY(-2f, 5))
            ;

        mainMenuSequence.Play().OnComplete(() => ZoomToPlayer());

    }

    public void ZoomToPlayer()
    {
        DOTween.To(orthoSize => currentCamera.orthographicSize = orthoSize, 5.82f, 1.3f, 2).OnComplete(() => DOTween.CompleteAll());
        currentCamera.GetComponent<Camera2DFollow>().enabled=true;
        playTextButton.GetComponent<Button>().interactable = false;
        //transform.DOMove(GameObject.FindGameObjectWithTag("player").transform.Find("Camera Target").transform.position, 2);
    }

    public void ZoomOutPlayer()
    {
        currentCamera.GetComponent<Camera2DFollow>().enabled = false;

        DOTween.To(orthoSize => currentCamera.orthographicSize = orthoSize, 1.3f, 5.82f, 2)
            .OnComplete(() => BackToMainMenu());
        //transform.DOMove(GameObject.FindGameObjectWithTag("player").transform.Find("Camera Target").transform.position, 2);
    }

    public void BackToMainMenu()
    {
        
        Debug.Log("Back to main menu pressed");

        Sequence backToMainMenuSequence = DOTween.Sequence();

        backToMainMenuSequence
            .Append(currentCamera.transform.DOMoveY(10, 5))
            
            .Join(title.DOFade(1, 3))
            .Append(playTextButton.DOFade(1, 1f))
            ;

        backToMainMenuSequence.Play().OnComplete(()=> playTextButton.GetComponent<Button>().interactable = false );


    }
}
