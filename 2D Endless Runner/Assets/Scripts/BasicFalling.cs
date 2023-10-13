using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFalling : MonoBehaviour
{
    public float speed;
    public float multipler;
    public bool dead;
    public float spawnOffset;
    private void Start()
    {
        dead = false;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * multipler * Time.deltaTime, transform.position.z);
        if (transform.position.y < -20)
        {
            dead = true;
        }
    }

    //Moves the given object based on the offset it needs (closer to the middle)
    public void moveOffset()
    {
        if (transform.position.x > 0)
        {
            transform.position = new Vector2(transform.position.x - spawnOffset, transform.position.y);
        }
        else if (transform.position.x < 0)
        {
            transform.position = new Vector2(transform.position.x + spawnOffset, transform.position.y);
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
