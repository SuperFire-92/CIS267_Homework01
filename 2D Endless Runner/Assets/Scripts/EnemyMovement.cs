using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private void Start()
    {
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
    }
}
