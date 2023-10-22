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
    public GameObject bullet;
    public GameObject blueShield;
    public List<GameObject> miniWalls;
    public List<GameObject> bullets;
    public float miniWallOffset;
    public float speed;
    public bool dead;
    public int numOfBullets;
    public int numOfShields;
    
    // Start is called before the first frame update
    void Start()
    {
        //Let the game know the player has not died
        dead = false;
        //Set the number of bullets to be 0
        numOfBullets = 0;
        //Set the number of shields to be 0
        numOfShields = 0;
        //Make sure the shield is invisible
        activateShield();
        //Store the player's rigidbody
        playerRigidBody = GetComponent<Rigidbody2D>();
        //Set the initial velocity
        playerRigidBody.velocity = new Vector2 (-1 * speed, playerRigidBody.velocity.y);
        //Create the list to hold miniWalls
        miniWalls = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
        shootBullet();
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

    //Remove bullets from the list
    public void updateBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] == null)
            {
                bullets.RemoveAt(i);
            }
        }
    }

    //Pause all current bulelts (runs when player dies)
    public void pauseBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].GetComponent<Bullet>().pauseBullet();
            }
        }
    }

    //Shoot a bullet using Space
    private void shootBullet()
    {
        if (!dead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (numOfBullets > 0)
                {
                    GameObject newBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 0.5f), new Quaternion(0f,0f,0f,1f));
                    bullets.Add(newBullet);
                    numOfBullets--;
                    gameManager.GetComponent<GameManager>().setNumOfBullets(numOfBullets);
                }
            }
        }
        updateBullets();
        
    }

    //To run when the player gets hit by a kill collision
    public void getHit(Collider2D collision)
    {
        //If the player has a shield, do not die
        if (numOfShields >= 3)
        {
            //Break the object if it's breakable
            if (collision.CompareTag("KillBreakable"))
            {
                Destroy(collision.gameObject);
                numOfShields = 0;
                activateShield();
                gameManager.GetComponent<GameManager>().setNumOfShields(numOfShields);
            }
            //Teleport the player to the center if it was unbreakable (if they did break this object, they would go out of bounds)
            else if (collision.CompareTag("Kill"))
            {
                transform.position = new Vector2(0, transform.position.y);
                numOfShields = 0;
                activateShield();
                gameManager.GetComponent<GameManager>().setNumOfShields(numOfShields);
            }
        }
        //If the player has no shield, die
        else
        {
            dead = true;
            playerRigidBody.velocity = new Vector2(0, 0);
            pauseBullets();
        }
    }

    //Give the player a blue shield around them if they have a shield
    public void activateShield()
    {
        if (numOfShields >= 3)
        {
            blueShield.SetActive(true);
        }
        else
        {
            blueShield.SetActive(false);
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
        //Delete all miniwalls that no longer exist
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
        //If the collided trigger is a kill trigger, kill the player
        if (collision.gameObject.CompareTag("Kill") || collision.gameObject.CompareTag("KillBreakable"))
        {
            getHit(collision);
        }

        //If the collision is a collectable
        if (collision.gameObject.CompareTag("Collectable"))
        {
            //Take the Collectable script
            Collectable col = collision.gameObject.GetComponent<Collectable>();

            //If the collectableType is 0, its a coin. Add 5 to the score.
            if (col.collectableType == 0)
            {
                gameManager.GetComponent<GameManager>().addScore(5);
            }
            //If the collectableType is 1, it's a slowdown powerup. Slow down the game for 2 seconds.
            if (col.collectableType == 1)
            {
                gameManager.GetComponent<GameManager>().setSlowdownTimer(2f);
            }
            //If the collectableType is 2, it's a bullet powerup. Add a bullet to the player's ammo.
            if (col.collectableType == 2)
            {
                if (numOfBullets < 3)
                {
                    numOfBullets++;
                }
                gameManager.GetComponent<GameManager>().setNumOfBullets(numOfBullets);
            }
            //If the collectableType is 3, it's a shield powerup. Add a point to our shields.
            if (col.collectableType == 3)
            {
                if (numOfShields < 3)
                {
                    numOfShields++;
                }
                activateShield();
                gameManager.GetComponent<GameManager>().setNumOfShields(numOfShields);
            }
        }
    }
}
