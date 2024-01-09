using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnQuiit()
    {
        Application.Quit();
    }

    public void OnGamePause()
    {
        Time.timeScale = 0f;
    }

    public void OnGameResume()
    {
        Time.timeScale = 1f;
    }
}
