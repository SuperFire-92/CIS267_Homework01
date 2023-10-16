using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //For the restart game button to reload the scene.
    public void restartGameButton()
    {
        SceneManager.LoadScene("2D Runner");
    }

    public void mainMenuButtion()
    {
        SceneManager.LoadScene("Start Menu");
    }

}
