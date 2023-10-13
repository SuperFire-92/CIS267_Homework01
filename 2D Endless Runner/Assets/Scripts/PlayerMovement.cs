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
    public List<GameObject> miniWalls;
    public float miniWallOffset;
    public float speed;
    public bool dead;
    
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = new Vector2 (-1 * speed, playerRigidBody.velocity.y);
        miniWalls = new List<GameObject>();
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
            //Update all miniwalls
            updateWalls();
        }
    }

    //Make a cute lil miniWall
    public void makeWall(bool LoR)
    {
        GameObject curMini = Instantiate(miniWall);
        curMini.GetComponent<Transform>().position = new Vector3(transform.position.x - (LoR ? -miniWallOffset : miniWallOffset), transform.position.y, transform.position.z);
        curMini.GetComponent<MiniWall>().speed = gameManager.GetComponent<GameManager>().speed;
        curMini.GetComponent<MiniWall>().multipler = gameManager.GetComponent<GameManager>().multipler;
        miniWalls.Add(curMini);
    }

    public void updateWalls()
    {
        //Delete all miniwalls that no longer exist (due to the way Lists work, can miss some)
        for (int i = 0; i < miniWalls.Count; i++)
        {
            if (miniWalls[i] == null)
            {
                miniWalls.RemoveAt(i);
            }
        }
        //Fix all wall's current multipler (for time powerup)
        for (int i = 0; i < miniWalls.Count; i++)
        {
            //Make sure the miniWall still exists
            if (miniWalls[i] != null)
            {
                miniWalls[i].GetComponent<MiniWall>().multipler = gameManager.GetComponent<GameManager>().multipler;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Kill"))
        {
            dead = true;
            playerRigidBody.velocity = new Vector2(0, 0);
        }

        if (collision.gameObject.CompareTag("Collectable"))
        {
            Collectable col = collision.gameObject.GetComponent<Collectable>();

            if (col.collectableType == 0)
            {
                gameManager.GetComponent<GameManager>().addScore(5);
            }
            if (col.collectableType == 1)
            {
                gameManager.GetComponent<GameManager>().slowdownTimer = 2f;
            }
        }
    }
}
