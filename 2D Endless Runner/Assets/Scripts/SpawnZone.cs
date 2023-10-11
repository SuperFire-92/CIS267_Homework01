using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public float speed;
    public bool dead;
    // Update is called once per frame
    private void Start()
    {
        dead = false;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        if (transform.position.y < -20)
        {
            dead = true;
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    public void pauseObject()
    {
        speed = 0;
    }
}
