using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Win()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
