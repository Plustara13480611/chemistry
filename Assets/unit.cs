using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public int maxJumps = 2;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite walkSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float moveInput;
    private int jumpCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        // เดินซ้าย / ขวา
        moveInput = Input.GetAxisRaw("Horizontal");

        // เปลี่ยน Sprite
        if (moveInput != 0)
        {
            spriteRenderer.sprite = walkSprite;

            if (moveInput > 0)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        // กระโดด
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    void Jump()
    {
        // กระโดดได้สูงสุด 2 ครั้ง
        if (jumpCount >= maxJumps)
            return;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce
        );

        jumpCount++;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // ตรวจว่ามีพื้นอยู่ใต้เท้าหรือไม่
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f &&
                collision.gameObject.CompareTag("Ground"))
            {
                // แตะพื้น -> รีเซ็ตจำนวนกระโดด
                jumpCount = 0;
                break;
            }
        }
    }
}