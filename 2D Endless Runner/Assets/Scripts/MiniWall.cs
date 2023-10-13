using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MiniWall : MonoBehaviour
{
    public float timer;
    public float speed;
    public float multipler;
    private SpriteRenderer renderer;
    private void Start()
    {
        timer = 0.5f;
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        timer = timer - Time.deltaTime * multipler;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f + timer * 2);

        transform.position = new Vector3(transform.position.x, transform.position.y - speed * multipler * Time.deltaTime, 0);

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
