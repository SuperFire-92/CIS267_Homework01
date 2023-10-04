using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public GameObject gameManager;
    public GameObject miniWall;
    public float miniWallOffset;
    public float speed;
    public bool dead;
    
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = new Vector2 (-1 * speed, playerRigidBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
    }

    private void changeDirection()
    {
        //If alive, let the player move
        if (!dead)
        {
            //If clicking A or Left, change trajectory to the left
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                float curDirection = playerRigidBody.velocity.x;
                playerRigidBody.velocity = new Vector2(-1 * speed, playerRigidBody.velocity.y);
                if (curDirection > 0)
                {
                    makeWall(true);
                }
            }
            //If clicking D or Right, change the trajectory to the right
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                float curDirection = playerRigidBody.velocity.x;
                playerRigidBody.velocity = new Vector2(1 * speed, playerRigidBody.velocity.y);
                if (curDirection < 0)
                {
                    makeWall(false);
                }
            }
        }
    }

    public void makeWall(bool LoR)
    {
        GameObject curMini = Instantiate(miniWall);
        curMini.GetComponent<Transform>().position = new Vector3(transform.position.x - (LoR ? -miniWallOffset : miniWallOffset), transform.position.y, transform.position.z);
        curMini.GetComponent<MiniWall>().speed = gameManager.GetComponent<GameManager>().speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Kill"))
        {
            dead = true;
            playerRigidBody.velocity = new Vector2(0, 0);
        }
    }
}
