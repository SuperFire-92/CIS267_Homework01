using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //To hold the mainMenu and controlMenu Canvases
    public Canvas mainMenu;
    public Canvas controlMenu;
    public GameObject background;
    public List<GameObject> backgroundList;
    private float bTimer;

    //================================================================================
    //Spawning in backgrounds to scroll through the main menu
    private void Start()
    {
        //Ensure the timescale is 1 (pausing and going to the main menu makes it 0f)
        Time.timeScale = 1.0f;
        //Create the backgroundlist
        backgroundList = new List<GameObject>();
        //Set the timer to create a new background to 0
        bTimer = 0;
        //Instantiate a new background with given values
        GameObject nb = Instantiate(background);
        nb.transform.position = new Vector2(0f, 0f);
        nb.GetComponent<Background>().backgroundSpeed = 7f/10f;
        nb.GetComponent<Background>().multipler = 1f;
        backgroundList.Add(nb);
        //Instantiate another background slightly higher
        nb = Instantiate(background);
        nb.transform.position = new Vector2(0f, 10f);
        nb.GetComponent<Background>().backgroundSpeed = 7f / 10f;
        nb.GetComponent<Background>().multipler = 1f;
        backgroundList.Add(nb);
    }

    private void Update()
    {
        //Subtract from the timer
        bTimer = bTimer - Time.deltaTime;
        if (bTimer <= 0)
        {
            //When the timer runs out, create a new background
            GameObject nb = Instantiate(background);
            nb.transform.position = new Vector2(0f, 20f);
            nb.GetComponent<Background>().backgroundSpeed = 7f/10f;
            nb.GetComponent<Background>().multipler = 1f;
            backgroundList.Add(nb);
            bTimer = 10.8f / (7f / 10);
        }
        //Delete backgrounds if they are "dead"
        for (int i = 0; i < backgroundList.Count; i++)
        {
            if (backgroundList[i].GetComponent<Background>().dead)
            {
                backgroundList[i].GetComponent<Background>().DestroyObject();
                backgroundList.RemoveAt(i);
            }
        }
    }
    //===============================================================================

    //Starts the game
    public void startGameButton()
    {
        SceneManager.LoadScene("2D Runner");
    }

    //Opens the control menu
    public void controlsButton()
    {
        mainMenu.gameObject.SetActive(false);
        controlMenu.gameObject.SetActive(true);
    }

    //Backs out of the control menu
    public void backFromControlsButton()
    {
        controlMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    //Exits the game
    public void exitGameButton()
    {
        Application.Quit();
    }
}
