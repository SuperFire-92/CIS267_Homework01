using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float multipler;
    private void Start()
    {
        multipler = 1f;
        GetComponent<BasicFalling>().speed = GetComponent<BasicFalling>().speed / 2;
        rb = GetComponent<Rigidbody2D>();

        float direction;
        if (transform.position.x != 0)
        {
            direction = transform.position.x / Mathf.Abs(transform.position.x);
        }
        else
        {
            direction = 1;
        }
        rb.velocity = new Vector2(speed * direction, 0);
    }

    private void Update()
    {
        if (GetComponent<BasicFalling>().speed == 0)
        {
            rb.velocity = Vector2.zero;
            Animator r = GetComponent<Animator>();
            r.speed = 0f;
        }

        if (GetComponent<BasicFalling>().multipler != multipler)
        {
            //Updates the speed of the enemy to account for a multipler
            updateMultipler();
        }
    }

    private void updateMultipler()
    {
        multipler = GetComponent<BasicFalling>().multipler;

        //Get the current direction of the enemy
        float direction;
        //This should return either 1 or -1, indicating the direction of the enemy
        if (rb.velocity.x != 0)
        {
            direction = rb.velocity.x / Mathf.Abs(rb.velocity.x);
        }
        else
        {
            direction = 1;
        }
        //Set the velocity of the enemy to the new multiplier's value
        rb.velocity = new Vector2(speed * direction * multipler, 0);
    }
}
