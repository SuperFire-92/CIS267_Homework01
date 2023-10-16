using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool dead;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Currently does not work, frequently phases through objects without destroying them
        rb.velocity = new Vector2(0, speed);
        if (transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }
    }

    public void pauseBullet()
    {
        rb.velocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("KillBreakable"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
