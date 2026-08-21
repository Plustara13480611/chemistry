
using UnityEngine;

public class unit : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite walkSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // เริ่มต้นเป็นท่ายืน
        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        // รับปุ่ม A/D หรือ ลูกศรซ้าย/ขวา
        moveInput = Input.GetAxisRaw("Horizontal");

        // เปลี่ยน Sprite ตามการเคลื่อนที่
        if (moveInput != 0)
        {
            spriteRenderer.sprite = walkSprite;

            // หันหน้าตามทิศทาง
            if (moveInput > 0)
                spriteRenderer.flipX = false;
            else if (moveInput < 0)
                spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}