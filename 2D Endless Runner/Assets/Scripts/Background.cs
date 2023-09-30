using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float backgroundSpeed;
    public bool dead;

    private void Start()
    {
        dead = false;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - backgroundSpeed * Time.deltaTime, transform.position.z);
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
        backgroundSpeed = 0;
    }
}
