using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float speed;
    [SerializeField] private float highJump;
    [SerializeField] private float climbSpeed;

    bool facingRight;
    bool grounded;
    bool _isClimbable;

    [SerializeField] Rigidbody2D mybody;
    [SerializeField] Animator myanimator;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(message: ">>>START<<<");
        mybody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();

        facingRight = true;
    }

    void FixedUpdate()
    {
        
        float move = Input.GetAxis("Horizontal");
        float climb = Input.GetAxis("Vertical");


        Debug.Log(message: "run");
        myanimator.SetFloat("Speed", Mathf.Abs(move));
        mybody.velocity = new Vector2(move * speed, mybody.velocity.y);
        

        void flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        if (move > 0 && !facingRight)
        {
            Debug.Log(message: "turn right");
            flip();
        }
        else if (move < 0 && facingRight)
        {
            Debug.Log(message: "turn left");
            flip();
        }


        Debug.Log($"{_isClimbable && move * climb == 0}, {_isClimbable} {move * climb == 0}");
        if(_isClimbable && move==0 && climb == 0)
        {
            myanimator.speed = 0;
        }
        else
        {
            myanimator.speed = 1;
        }

        if(_isClimbable && Mathf.Abs(climb) > 0)
        {
            mybody.velocity = new Vector2(0, climb * climbSpeed);
        }


        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(message: "jump");
            if (grounded)
            {
                Debug.Log(message: "not on the ground");
                grounded = false;
                mybody.velocity = new Vector2(mybody.velocity.x, highJump);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) //va cham
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log(message: "on the ground");
            grounded = true;
        }
    }

    void die()
    {
        myanimator.SetBool("die", true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="DeadTouch")
        {
            Debug.Log(message: "die");
            die();
        }


        Debug.Log($"EXIT {col.tag} ====={col.gameObject.tag=="Climable"}");
        if (col.gameObject.tag == "Climable")
        {
            _isClimbable = true;
            mybody.gravityScale = 0;
            myanimator.SetBool("climbing", true);
            Debug.Log(_isClimbable+ "123");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log($"EXIT {col.tag} ====={col.gameObject.tag == "Climable"}");
        if (col.gameObject.tag == "Climable")
        {
            _isClimbable = false;
            mybody.gravityScale = 1;
            myanimator.SetBool("climbing", false);
            Debug.Log(_isClimbable + "456");
        }
    }




}
