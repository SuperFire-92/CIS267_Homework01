using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Canvas pauseGameDisplay;
    //For the restart game button to reload the scene.
    public void restartGameButton()
    {
        SceneManager.LoadScene("2D Runner");
    }

    //Opens the main menu
    public void mainMenuButtion()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void continueGameButton()
    {
        Time.timeScale = 1.0f;
        pauseGameDisplay.gameObject.SetActive(false);
    }

}
