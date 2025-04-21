using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    [Header("Wall Slide Settings")]
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 0.2f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isWallSliding;
    private bool isTouchingWall;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        HandleJump();
        HandleWallSlide();
        HandleFlip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void HandleWallSlide()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundLayer);
        isWallSliding = isTouchingWall && !IsGrounded() && horizontal != 0f;
    }

    private void HandleFlip()
    {
        if (horizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Este Flip vai inverter o personagem
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Debug opcional
        // Debug.Log("Virou: " + (isFacingRight ? "Direita" : "Esquerda"));
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
