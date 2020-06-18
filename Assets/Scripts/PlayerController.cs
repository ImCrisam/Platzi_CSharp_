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
    BoxCollider2D colliderbase;
    SpriteRenderer sprite;
    public LayerMask groundMask;
    float height, limitHeight, velocityx, axix, sizeBaseFormTheCenter;
    Vector3 Lastposition;
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
        Lastposition = this.transform.position;
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    public void StartGame()
    {
        Invoke("RestarPosition", 0.3f);
        Invoke("RestarCameraPosition", 0.4f);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

    }
    void RestarPosition()
    {
        rigidBody.velocity = Vector2.zero;
        this.transform.position = Lastposition;
    }
    void RestarCameraPosition()
    {
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
    bool IsTouchingTheGround()
    {
        temporalV3 = this.transform.position;
        temporalV3.x = temporalV3.x-sizeBaseFormTheCenter;
        temporalV3.y = temporalV3.y-limitHeight;
        return Physics2D.Raycast(temporalV3,
                                    Vector2.right,
                                    sizeBaseFormTheCenter*2,
                                    groundMask);
                

    }


}
