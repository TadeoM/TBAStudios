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
    Sequence mainMenuSequence;

    // Use this for initialization
    void Start () {
        currentCamera = Camera.main;
        mainMenuSequence = DOTween.Sequence();
        mainMenuSequence.SetAutoKill(false);
    }
	
	// Update is called once per frame
	void Update () {
        // Dev Controls
        if (Input.GetKeyDown(KeyCode.X))
        {
            BackToMainMenu();
        }
	}

    public void PlayGame()
    {
        Debug.Log("Play game button pressed");

        mainMenuSequence
            .Append(playTextButton.DOFade(0, .5f))
            .Append(title.DOFade(0, 2))
            .Append(currentCamera.transform.DOMoveY(-7.2f, 5))

            ;

        mainMenuSequence.Play();
        // fade the button
        // playTextButton.DOFade(0, 2);

        // fade the title
        // title.DOFade(0, 2);

        // pan the building
        // currentCamera.transform.DOMoveY(-7.2f, 5);
    }

    public void BackToMainMenu()
    {
        // pan the building
        //currentCamera.transform.DOMoveY(-7.2f, 3);

        // fade the title
        //title.DOFade(0, 2);

        // fade the button
        //playTextButton.DOFade(0, 2);
        Debug.Log("Back to main menu pressed");
        mainMenuSequence.Goto(mainMenuSequence.Duration());
        mainMenuSequence.PlayBackwards();

    }
}
