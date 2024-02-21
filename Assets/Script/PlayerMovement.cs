using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementClass movement;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    RaycastHit2D raycastHit2D;
    float horizontalInput;
    float wallJumpCooldown;
    float initialGravity;

    // Start is called before the first frame update

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        initialGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Movement(movement.speed);
        WallJump();
    }

    public void Movement(float velocity)
    {
        //rb.velocity = new Vector2(input * velocity, rb.velocity.y);
        if (horizontalInput < -.01f) //flip left
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (horizontalInput > .01f) //flip right
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        anim.SetBool("isRunning", rb.velocity.x > 0.1f || rb.velocity.x < -.1f);
    }

    public void WallJump()
    {

        if(wallJumpCooldown > .2f)
        {
            rb.velocity = new Vector2(horizontalInput * movement.speed, rb.velocity.y);

            if(IsOnWall() && !IsGrounded())
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = initialGravity;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                NormalJump(movement.jumpForce);
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    public void NormalJump(float jumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded()) //if grounded
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetTrigger("jumpTrigger");
            }
            else
            {
                if(IsOnWall()) //if not grounded and stik to the wall
                {
                    if(horizontalInput == 0)
                    {
                        wallJumpCooldown = 0f;
                        rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * movement.speed, 0f);
                    }
                    else
                    {
                        wallJumpCooldown = 0f;
                        rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * movement.speed, movement.speed * 2f);
                    }
                    //mathf (if f > 0 .. then it will be 1.. f <= 0 then it will be -1)
                }
            }
        }

        anim.SetBool("isGrounded", IsGrounded());
    }

    bool IsGrounded()
    {
        raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    bool IsOnWall()
    {
        raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x, 0), .1f, wallLayer);
        return raycastHit2D.collider != null;
    }

    public bool CanAttack()
    {
        return IsGrounded() && horizontalInput == 0 && !IsOnWall();
    }

    [System.Serializable]
    public class MovementClass
    {
        public float speed = 5f;
        public float jumpForce = 8f;
    }
}