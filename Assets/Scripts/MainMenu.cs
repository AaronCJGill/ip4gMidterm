using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void playButton()
    {
        SceneManager.LoadScene(1);
    }
    public void mainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void exitButton()
    {
        Application.Quit();
    }


}
