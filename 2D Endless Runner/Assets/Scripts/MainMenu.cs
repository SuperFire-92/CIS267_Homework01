using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGameButton()
    {
        SceneManager.LoadScene("2D Runner");
    }

    public void openControls()
    {

    }

    public void exitGameButton()
    {
        Application.Quit();
    }
}
