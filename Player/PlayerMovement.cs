using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask ladderLayer;

    public float speed = 6f;
    public float climbSpeed = 3f;
    public float jump = 6f;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider2D;

    private float landingTimer;
    public float landingDelay = 1.5f;

    private float originalGravity;
    private bool isClimbing;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        originalGravity = body.gravityScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (onLadder())
        {
            if (Mathf.Abs(verticalInput) > 0.03f)
            {
                isClimbing = true;
            }

            if (isGrounded() && verticalInput <= 0.03f)
            {
                isClimbing = false;
            }
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing)
        {
            body.gravityScale = 0f;
            body.linearVelocity = new Vector2(0f, verticalInput * climbSpeed);

            if (Mathf.Abs(verticalInput) > 0.03f)
                anim.speed = 1f;
            else
                anim.speed = 0f;
        }
        else
        {
            anim.speed = 1f;
            body.gravityScale = originalGravity;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }

        if (!isClimbing && Mathf.Abs(horizontalInput) > 0.01f)
        {
            if (horizontalInput > 0f)
                transform.localScale = new Vector3(7, 7, 7);
            else
                transform.localScale = new Vector3(-7, 7, 7);
        }
                
        if (landingTimer > 0)
            landingTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && landingTimer <= 0 && !isClimbing)
        {
            Jump();
            SoundManager.instance.PlaySound(jumpSound);
        }

        anim.SetBool("run", horizontalInput != 0 && !isClimbing);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("climbing", isClimbing);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(
            body.linearVelocity.x,
            jump
        );

        anim.SetTrigger("jump");
        landingTimer = landingDelay;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onLadder()
    {
        Collider2D hit = Physics2D.OverlapBox(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, ladderLayer);
        return hit != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onLadder();
    }

}