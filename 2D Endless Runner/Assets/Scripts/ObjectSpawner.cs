using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //This class spawns any object needed in the game.
    public List<GameObject> listOfSpawnableObjects;
    private GameObject spawnedObject;
    public float speed;

    private void Start()
    {
        spawnObject();
    }

    public void spawnObject()
    {
        int whatToSpawn = Random.Range(0, 6);
        if (whatToSpawn < 3)
        {
            //Spawn nothing
        }
        else if (whatToSpawn == 4) 
        {
            //Spawn an enemy/blockade
            spawnedObject = Instantiate(listOfSpawnableObjects[0]);
            spawnedObject.GetComponent<Transform>().localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            spawnedObject.GetComponent<BasicFalling>().speed = speed;
        }
        else if (whatToSpawn == 5)
        {
            //Spawn a collectable
            spawnedObject = Instantiate(listOfSpawnableObjects[1]);
            spawnedObject.GetComponent<Transform>().localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            spawnedObject.GetComponent<BasicFalling>().speed = speed;
        }
    }

    public void DestroyObject()
    {
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<BasicFalling>().DestroyObject();
        }
    }
}
