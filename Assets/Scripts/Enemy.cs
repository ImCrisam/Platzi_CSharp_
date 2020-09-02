using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damager = 20;
    public float distanceRay = 0.4f, velocity = 2f;
    float rayCastDown;
    Vector2 position, pointLeft, pointRight;
    float height, width, scaleHeight, ScaleWidth;
    new Rigidbody2D rigidbody;
    SpriteRenderer sprite;
    PlayerController player;

    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        height = (this.GetComponent<CapsuleCollider2D>().size.y * this.transform.localScale.y) / 2;
        width = (this.GetComponent<CapsuleCollider2D>().size.x * this.transform.localScale.x) / 2;
        rayCastDown = height + distanceRay;
        rigidbody.velocity = new Vector2(-velocity, 0);
        
    }
    

    // Update is called once per frame

   
    private void FixedUpdate() {

        if (IsOnTheEdge())
        {
            fixX();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground"))){
            fixX();
        }
        else if (other.tag.Equals("Player") )
        {
            this.GetComponent<AudioSource>().Play();
            if(other is BoxCollider2D){
            player.subtractHealth(damager);
            player.Jump();
            }else{
            player.subtractHealth(damager);

            player.JumpHorizontal(player.GetVelocityX()>0 ? true : false);
            }
        }
    }
    private bool IsOnTheEdge()
    {
        pointRight = this.transform.position;
        pointLeft = this.transform.position;

        pointLeft.x -= width;
        pointRight.x += width;
        Debug.DrawRay(pointLeft, Vector2.down * rayCastDown, Color.magenta);
        Debug.DrawRay(pointRight, Vector2.down * rayCastDown, Color.magenta);
        if (Physics2D.Raycast(pointRight, Vector2.down, rayCastDown, groundMask)
            == Physics2D.Raycast(pointLeft, Vector2.down, rayCastDown, groundMask))
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private void fixX()
    {
        sprite.flipX = !sprite.flipX;
        rigidbody.velocity = -rigidbody.velocity;
    }
}
