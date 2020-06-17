using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_VELOCITY_Y = "velocityY";


    public float jumpForce = 7f;
    public float runForce = 1f;
    public float maxRun = 5f;
    Rigidbody2D rigidBody;
    CapsuleCollider2D colliderPlayer;
    SpriteRenderer sprite;
    public LayerMask groundMask;
    float height, limitHeight, velocityx, axix;
    Vector3 Lastposition;
    Animator animator;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        height = colliderPlayer.size.y;
        limitHeight = height / 3;
        Lastposition = this.transform.position;
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    public void StartGame(){
     Invoke("RestarPosition", 0.3f);
    }
    void RestarPosition(){
        this.transform.position = Lastposition;
        rigidBody.velocity = Vector2.zero;
        GameObject camera = GameObject.FindWithTag("MainCamera");
        camera.GetComponent<CameraFollow>().ResetCameraPosition();
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

            if (velocityx < maxRun && velocityx > -maxRun && animator.GetBool(STATE_ON_THE_GROUND))
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

            Debug.DrawRay(this.transform.position, Vector2.down * limitHeight, Color.magenta);
        }else if (GameManager.instanceGameManager.currentGameState.Equals(GameState.menu)){
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

    public void Die(){
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.instanceGameManager.GameOver();
    }

    public void SetAlive(bool alive){
        animator.SetBool(STATE_ALIVE, alive);
    }
    bool IsTouchingTheGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, limitHeight, groundMask);
    }


}
