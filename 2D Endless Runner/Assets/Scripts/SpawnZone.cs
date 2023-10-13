using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public float speed;
    public float multipler;
    public float scoreOnSpawn;
    public bool dead;
    public GameObject[] listOfEnemies;
    public GameObject[] listOfCollectables;
    private GameObject spawnedObject;
    private bool canSpawn;
    private bool enemyOrCollectable;

    private void Start()
    {
        dead = false;

        //Decide if we can spawn an object on this SpawnZone
        if (canSpawnObject())
        {
            //If we can spawn an object, decide what kind
            decideEnemyOrCollectable();

            //Now that we know if it's going to be an enemy or collectable, spawn the object
            //This line checks if enemyOrCollectable is true or false, and spawns either an enemy from the list of enemies or
            //a collectable from the list of collectables (the list must contain at least one item).
            spawnedObject = Instantiate(enemyOrCollectable ? listOfEnemies[Random.Range(0, listOfEnemies.Length)] : listOfCollectables[Random.Range(0, listOfCollectables.Length)]);
            spawnedObject.transform.position = transform.position;
            spawnedObject.GetComponent<BasicFalling>().speed = speed;
            spawnedObject.GetComponent<BasicFalling>().moveOffset();
            spawnedObject.GetComponent<BasicFalling>().multipler = multipler;
        }

    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * multipler * Time.deltaTime, transform.position.z);
        if (spawnedObject != null )
        {
            if (spawnedObject.transform.position.y < -20)
            {
                dead = true;
            }
        }
        else
        {
            if (transform.position.y < -20)
            {
                dead = true;
            }
        }
    }

    public void updateMultipler(float m)
    {
        multipler = m;
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<BasicFalling>().multipler = m;
        }
    }

    public bool canSpawnObject()
    {
        int spawn = Random.Range(0, 10);
        if (spawn > 7 - (int)(scoreOnSpawn / 25))
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }

        return canSpawn;
    }
    public bool decideEnemyOrCollectable()
    {
        int spawn = Random.Range(0, 3);
        if (spawn < 2)
        {
            enemyOrCollectable = false;
        }
        else
        {
            enemyOrCollectable = true;
        }
        return enemyOrCollectable;
    }

    public void DestroyObject()
    {
        if (spawnedObject != null)
        {
            if (spawnedObject.GetComponent<BasicFalling>().dead == true)
            {
                Destroy(spawnedObject);
            }
        }
        Destroy(this.gameObject);
    }

    public void pauseObject()
    {
        speed = 0;
        if (spawnedObject != null)
        {
            //Make the object this spawner spawned's speed be 0
            spawnedObject.GetComponent<BasicFalling>().speed = 0;
        }
    }
}
