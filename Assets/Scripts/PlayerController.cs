using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_VELOCITY_Y = "velocityY";

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 50,
                     MAX_HEALTH = 200, MAX_MANA = 100,
                     MIN_HEALTH = 10, MIN_MANA = 0;

    private int healthPoints, manaPoints;



    public float jumpForce = 7f, runForce = 1f, runForcePower = 2f;

    public float maxRun = 5f, maxRunPower = 8f;
    float floatTemporal;
    public int costMana = 1;
    Rigidbody2D rigidBody;
    CapsuleCollider2D colliderPlayer;
    BoxCollider2D colliderbase;
    SpriteRenderer sprite;
    public LayerMask groundMask;
    float height, limitHeight, velocityx, axix, sizeBaseFormTheCenter;
    public Transform Lastposition;
    Vector3 temporalV3;
    Vector2 temporalV2, sizePlayerAlive;
    Animator animator;
    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        colliderPlayer = this.GetComponent<CapsuleCollider2D>();
        colliderbase = this.GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        sizePlayerAlive = colliderPlayer.size;
        sizeBaseFormTheCenter = colliderbase.size.x / 2;
        height = colliderPlayer.size.y;
        limitHeight = height / 2.3f;
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);

    }

    public void StartGame()
    {
        this.transform.position = Lastposition.position;
        rigidBody.velocity = Vector2.zero;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;
        ConfiCollider(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instanceGameManager.currentGameState.Equals(GameState.inGame))
        {

            if (Input.GetButtonDown("Jump") && IsTouchingTheGround())
            {
                Jump();
            }

            axix = Input.GetAxis("Horizontal");
            if (axix != 0 && Input.GetButton("Run"))
            {
                if (manaPoints >= costMana)
                {
                    manaPoints -= costMana;
                    Run(axix, runForcePower, maxRunPower);

                }
                else
                {
                    Run(axix, runForce, maxRun);
                }
            }
            else if (axix != 0)
            {
                Run(axix, runForce, maxRun);
            }


            animator.SetFloat(STATE_VELOCITY_Y, rigidBody.velocity.y);
            animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
            sprite.flipX = velocityx < 0;
            temporalV3 = this.transform.position;
            temporalV3.x = temporalV3.x - sizeBaseFormTheCenter;
            temporalV3.y = temporalV3.y - limitHeight;
            Debug.DrawRay(temporalV3, Vector2.right * sizeBaseFormTheCenter * 2, Color.magenta);
        }
        else if (GameManager.instanceGameManager.currentGameState.Equals(GameState.menu))
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    public void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    public void JumpHorizontal(bool left)
    {
        temporalV2 = Vector2.one;
        temporalV2.x = left ? -temporalV2.x : temporalV2.x;
        rigidBody.AddRelativeForce(temporalV2 * (jumpForce / 2), ForceMode2D.Impulse);
        //rigidBody.AddForce(temporalV2* (jumpForce/2), ForceMode2D.Impulse);
    }

    void Run(float axix, float force, float max)
    {
        velocityx = rigidBody.velocity.x;
        if (velocityx < max && velocityx > -max)
        {
            rigidBody.AddForce(Vector2.right * force * axix, ForceMode2D.Impulse);

        }
        else
        {
            rigidBody.velocity = new Vector2(Vector2.ClampMagnitude(rigidBody.velocity, max).x, rigidBody.velocity.y);

        }
    }

    public void Die()
    {
        float travelled = GameManager.instanceGameManager.score;
        float prevTravelled = PlayerPrefs.GetFloat("maxScorre", 0);
        PlayerPrefs.SetFloat("maxScorre", travelled > prevTravelled ? travelled : prevTravelled);
        this.ConfiCollider(false);
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.instanceGameManager.GameOver();
        
    }

    public void SetAlive(bool alive)
    {
        animator.SetBool(STATE_ALIVE, alive);
    }

    public void ConfiCollider(bool alive){
        colliderbase.isTrigger = !alive;
        colliderPlayer.direction = alive ? CapsuleDirection2D.Vertical:CapsuleDirection2D.Horizontal ;
        colliderPlayer.size = !alive ? new Vector2(sizePlayerAlive.y,sizePlayerAlive.x):sizePlayerAlive;
    }
   
    public bool IsTouchingTheGround()
    {
        temporalV3 = this.transform.position;
        temporalV3.x = temporalV3.x - sizeBaseFormTheCenter;
        temporalV3.y = temporalV3.y - limitHeight;
        return Physics2D.Raycast(temporalV3,
                                    Vector2.right,
                                    sizeBaseFormTheCenter * 2,
                                    groundMask);
    }
    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if (this.healthPoints > MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void subtractHealth(int point)
    {
        if (GameManager.instanceGameManager.currentGameState.Equals(GameState.inGame))
        {
            this.healthPoints -= point;
            if (this.healthPoints <= 0)
            {
                Die();
            }
        }
    }
    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints > MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }
    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelDistrance()
    {
        return this.transform.position.x - Lastposition.position.x;
    }
    public float GetVelocityX()
    {
        return velocityx;
    }






}
