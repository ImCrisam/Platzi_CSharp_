using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_VELOCITY_Y = "velocityY";

    public const int INITIAL_HEALTH =100, INITIAL_MANA=15,
                     MAX_HEALTH = 200, MAX_MANA = 60,
                     MIN_HEALTH =10, MIN_MANA=0;

    private int healthPoints, manaPoints;



    public float jumpForce = 7f;
    public float runForce = 1f;
    public float maxRun = 5f;
    Rigidbody2D rigidBody;
    CapsuleCollider2D colliderPlayer;
    BoxCollider2D colliderbase;
    SpriteRenderer sprite;
    public LayerMask groundMask;
    float height, limitHeight, velocityx, axix, sizeBaseFormTheCenter;
    public Transform Lastposition;
    Vector3 temporalV3;
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
     
    }
   
   
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instanceGameManager.currentGameState.Equals(GameState.inGame))
        {

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            velocityx = rigidBody.velocity.x;
            axix = Input.GetAxis("Horizontal");

            if (velocityx < maxRun && velocityx > -maxRun)
            {
                rigidBody.AddForce(Vector2.right * runForce * axix, ForceMode2D.Impulse);

            }
            else
            {
                rigidBody.velocity = new Vector2(Vector2.ClampMagnitude(rigidBody.velocity, maxRun).x, rigidBody.velocity.y);

            }

            animator.SetFloat(STATE_VELOCITY_Y, rigidBody.velocity.y);
            animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
            sprite.flipX = velocityx < 0;
            temporalV3 = this.transform.position;
            temporalV3.x = temporalV3.x-sizeBaseFormTheCenter;
            temporalV3.y = temporalV3.y-limitHeight;
            Debug.DrawRay(temporalV3, Vector2.right * sizeBaseFormTheCenter*2, Color.magenta);
        }
        else if (GameManager.instanceGameManager.currentGameState.Equals(GameState.menu))
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    void Jump()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Die()
    {
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.instanceGameManager.GameOver();
    }

    public void SetAlive(bool alive)
    {
        animator.SetBool(STATE_ALIVE, alive);
    }
    public bool IsTouchingTheGround()
    {
        temporalV3 = this.transform.position;
        temporalV3.x = temporalV3.x-sizeBaseFormTheCenter;
        temporalV3.y = temporalV3.y-limitHeight;
        return Physics2D.Raycast(temporalV3,
                                    Vector2.right,
                                    sizeBaseFormTheCenter*2,
                                    groundMask);
                

    }
    public void CollectHealth(int points){
        this.healthPoints += points;
        if(this.healthPoints>MAX_HEALTH){
            this.healthPoints = MAX_HEALTH;
        }
    }
    public void CollectMana(int points){
        this.manaPoints += points;
        if(this.manaPoints>MAX_MANA){
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth(){
        return healthPoints;
    } 
    public int GetMana(){
        return manaPoints;
    }

    public float GetTravelDistrance(){
        return this.transform.position.x - Lastposition.position.x;
    }

    


  
  

}
