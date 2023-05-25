 using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private bool doubleJump;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState {idle, running, jumping, falling };

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource collectSoundEffect;

    private bool m_FacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);



        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                doubleJump = true;
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            }

            else if (doubleJump)
            {
                doubleJump = false;
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        MovementState state = MovementState.idle;

        // Debug.Log(rb.velocity.y);

        if (dirX > 0 && !m_FacingRight)
        {
            Flip();
            state = MovementState.running;
            //sprite.flipX = false;
            
        }
        else if (dirX < 0 && m_FacingRight)
        {
            Flip();
            state = MovementState.running;
            //sprite.flipX = true;
            
        }
        else if (dirX > 0)
        {
            state = MovementState.running;
        }
        else if(dirX < 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 1f)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -5f)
        {
            state = MovementState.falling;

        }
        anim.SetInteger("state", (int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            moveSpeed = 12f; 
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
            StartCoroutine(ResetPower());
        }

        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            jumpForce = 20f;
            GetComponent<SpriteRenderer>().material.color = Color.magenta;
            StartCoroutine (ResetPower());  
        }


    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        moveSpeed = 7f; 
        jumpForce = 14f;
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    private void Flip() 
    {
        m_FacingRight = !m_FacingRight;
        rb.transform.Rotate(0, 180, 0);
    }
}