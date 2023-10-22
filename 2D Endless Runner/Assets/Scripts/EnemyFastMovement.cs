using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFastMovement : MonoBehaviour
{
    public float speedMultiplier;
    private BasicFalling bf;
    public GameObject warning;
    private GameObject createdWarning;
    private float timer;

    private void Start()
    {
        customizeBF();
        spawnWarning();
        timer = 0.5f;
    }

    private void Update()
    {
        if (createdWarning != null)
        {
            createdWarning.transform.position = new Vector2(transform.position.x, 4.2f);
            timer = timer - Time.deltaTime;
            if (timer < 0f)
            {
                createdWarning.SetActive(!createdWarning.active);
                timer = 0.5f;
            }
            if (transform.position.y < 4.2f)
            {
                Destroy(createdWarning);
            }
        }
    }

    public void customizeBF()
    {
        bf = GetComponent<BasicFalling>();
        bf.speed = bf.speed * speedMultiplier;
        bf.spawnOffset = Random.Range(0, 3);
        bf.moveOffset();
        transform.position = new Vector2(transform.position.x, transform.position.y + 5);
    }

    public void spawnWarning()
    {
        createdWarning = Instantiate(warning, this.transform, worldPositionStays:true);
        createdWarning.transform.position = new Vector2(transform.position.x, 4.2f);
    }
}
