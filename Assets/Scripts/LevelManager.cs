using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level { MainGame, CatMiniGame, CuttingMiniGame, DialogMiniGame, HoardingMiniGame, Credits }

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance;


    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Dev controls
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene((int)Level.MainGame);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadScene((int)Level.CuttingMiniGame);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadScene((int)Level.DialogMiniGame);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadScene((int)Level.HoardingMiniGame);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadScene((int)Level.Credits);

        }

    }
    
    public void LoadScene(Level level)
    {
        LoadScene((int)level);

    }

    public void LoadScene(int i)
    {
        if(i < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(i);
        }
        
    }
}
