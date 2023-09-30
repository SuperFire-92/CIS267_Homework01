using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wallSpeed;
    public bool dead;
    // Update is called once per frame
    private void Start()
    {
        dead = false;
        if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - wallSpeed * Time.deltaTime, transform.position.z);
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
        wallSpeed = 0;
    }
}
