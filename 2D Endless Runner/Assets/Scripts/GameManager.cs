using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private int score;
    public bool gameEnd;
    public float speed;
    public GameObject player;
    public GameObject wall;
    public GameObject background;
    public GameObject spike;
    public GameObject gameOverDisplay;
    private int distance;
    private int oldDistance;
    private float distanceTimer;
    private float wallTimer;
    private float backgroundTimer;
    private bool changedDistance;
    //These lists contain references to every object of its type
    public List<GameObject> walls;
    public List<GameObject> backgrounds;
    public List<GameObject> spikes;

    private void Start()
    {
        //Make sure the game is running
        gameEnd = false;
        //Set the changedDistance to false
        changedDistance = false;
        //Set the distance between walls to start with
        distance = 5;
        //Create a randomly generated number for the distance timer
        resetDistanceTimer();
        //Set the wall timer to immediately make a new wall
        wallTimer = 0;
        //Do the same with the background timer
        backgroundTimer = 0;
        //Set the speed of the walls
        speed = 9;
        //Create the first two layers of walls
        createWalls(0);
        createWalls(10);
        //Create the first two layers of background
        createBackground(0);
        createBackground(10);
    }

    private void Update()
    {
        gameEnd = player.GetComponent<PlayerMovement>().dead;
        if (!gameEnd)
        {
            //Subtract time from both timers
            distanceTimer = distanceTimer - Time.deltaTime;
            wallTimer = wallTimer - Time.deltaTime;
            backgroundTimer = backgroundTimer - Time.deltaTime;
            if (distanceTimer <= 0)
            {
                oldDistance = distance;
                //If the distance timer has finished, create a new distance
                distance = Random.Range(5, 9);
                resetDistanceTimer();
                changedDistance = true;
            }
            if (wallTimer <= 0)
            {
                //If the wall timer has finished, create new walls.
                createWalls();
                resetWallTimer();
            }
            if (backgroundTimer <= 0)
            {
                //If the background timer has finished, create a new background
                createBackground(20);
                resetBackgroundTimer();
            }
        }
        else
        {
            endGame();
        }

    }

    public void endGame()
    {
        //Pause all objects
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponent<Wall>().pauseObject();
        }
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].GetComponent<Background>().pauseObject();
        }
        for (int i = 0; i < spikes.Count; i++)
        {
            spikes[i].GetComponent<Spike>().pauseObject();
        }

        //Add a game over display
        gameOverDisplay.SetActive(true);
    }

    //Resets the distance timer
    public void resetDistanceTimer()
    {
        distanceTimer = Random.Range(3, 3);
    }

    //Resets the wall timer
    public void resetWallTimer()
    {
        wallTimer = 1;
    }

    public void resetBackgroundTimer()
    {
        backgroundTimer = 12;
    }

    //Creates two new walls at the given x coordinates
    public void createWalls()
    {
        if (changedDistance)
        {
            createSpikes();
            changedDistance = false;
        }
        GameObject newWall = Instantiate(wall);
        newWall.GetComponent<Transform>().position = new Vector3 (distance, 20, 0);
        newWall.GetComponent<Wall>().wallSpeed = speed;
        walls.Add(newWall);
        newWall = Instantiate(wall);
        newWall.GetComponent<Transform>().position = new Vector3 (-distance, 20, 0);
        newWall.GetComponent<Wall>().wallSpeed = speed;
        walls.Add(newWall);

        destroyWalls();
    }

    //Creates two new walls at the given x and y coordinates
    public void createWalls(float objY) 
    {
        if (changedDistance)
        {
            createSpikes();
            changedDistance = false;
        }
        GameObject newWall = Instantiate(wall);
        newWall.GetComponent<Transform>().position = new Vector3 (distance, objY, 0);
        newWall.GetComponent<Wall>().wallSpeed = speed;
        walls.Add(newWall);
        newWall = Instantiate(wall);
        newWall.GetComponent<Transform>().position = new Vector3 (-distance, objY, 0);
        newWall.GetComponent<Wall>().wallSpeed = speed;
        walls.Add(newWall);

        destroyWalls();
    }

    public void createSpikes()
    {
        for (int i = oldDistance; i > distance; i--)
        {
            GameObject newSpike = Instantiate(spike);
            newSpike.GetComponent<Transform>().position = new Vector3(i-2.4f, 15.2f, 0);
            newSpike.GetComponent<Spike>().spikeSpeed = speed;
            spikes.Add(newSpike);
            newSpike = Instantiate(spike);
            newSpike.GetComponent<Transform>().position = new Vector3(-i+2.4f, 15.2f, 0);
            newSpike.GetComponent<Spike>().spikeSpeed = speed;
            spikes.Add(newSpike);
        }

        //for (int i = distance + 1; i >= oldDistance; i--)
        //{
        //    GameObject newSpike = Instantiate(spike);
        //    newSpike.GetComponent<Transform>().position = new Vector3(i - 2.5f, 15f, 0);
        //    newSpike.GetComponent<Spike>().spikeSpeed = speed;
        //    spikes.Add(newSpike);
        //    newSpike = Instantiate(spike);
        //    newSpike.GetComponent<Transform>().position = new Vector3(-i + 2.5f, 15f, 0);
        //    newSpike.GetComponent<Spike>().spikeSpeed = speed;
        //    spikes.Add(newSpike);
        //}

        destroySpikes();
    }

    public void destroySpikes()
    {
        for (int i = 0; i < spikes.Count; i++)
        {
            Spike curSpike = spikes[i].GetComponent<Spike>();
            if (curSpike.dead)
            {
                curSpike.DestroyObject();
                spikes.RemoveAt(i);
            }
        }
    }

    public void destroyWalls()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            Wall curWall = walls[i].GetComponent<Wall>();
            if (curWall.dead)
            {
                curWall.DestroyObject();
                walls.RemoveAt(i);
            }
        }
    }

    public void createBackground(float objY)
    {
        GameObject newBackground = Instantiate(background);
        newBackground.GetComponent<Transform>().position = new Vector3(0, objY, 0);
        newBackground.GetComponent<Background>().backgroundSpeed = speed / 10;
        backgrounds.Add(newBackground);

        destroyBackgrounds();
    }

    public void destroyBackgrounds()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            Background curBack = backgrounds[i].GetComponent<Background>();
            if (curBack.dead)
            {
                curBack.DestroyObject();
                backgrounds.RemoveAt(i);
            }
        }
    }
}
