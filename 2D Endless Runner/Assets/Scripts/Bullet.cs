using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool dead;

    private void Start()
    {

    }

    private void Update()
    {
        //Currently does not work, frequently phases through objects without destroying them
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        if (transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }
    }

    public void pauseBullet()
    {
        speed = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("KillBreakable"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
