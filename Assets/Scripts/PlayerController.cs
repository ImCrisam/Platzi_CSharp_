using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    const string STATE_ALIVE ="isALive";
    const string STATE_ON_THE_GROUND ="isOnTheGround";

    public float jumpForce = 6f;
    Rigidbody2D rigidBody;
    CapsuleCollider2D colliderPlayer;
    public LayerMask groundMask;
    float height, limitHeight;
    Animator animator;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        height = colliderPlayer.size.y;
        limitHeight = height/3;
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ){
            Jump();

        }
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        Debug.DrawRay(this.transform.position, Vector2.down*limitHeight, Color.magenta);
    }

    void Jump(){
        if(IsTouchingTheGround()){
        rigidBody.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);

        }
    }

    bool IsTouchingTheGround(){
        return Physics2D.Raycast(this.transform.position, Vector2.down, limitHeight, groundMask);
    }


}
