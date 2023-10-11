using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MiniWall : MonoBehaviour
{
    public float timer;
    public float speed;
    private SpriteRenderer renderer;
    private SpriteRenderer childRenderer;
    private void Start()
    {
        timer = 1;
        renderer = GetComponent<SpriteRenderer>();
        childRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        timer = timer - Time.deltaTime;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f - timer);
        childRenderer.color = new Color(childRenderer.color.r, childRenderer.color.g, childRenderer.color.b, 0.33f - (timer * 0.33f));

        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, 0);

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
