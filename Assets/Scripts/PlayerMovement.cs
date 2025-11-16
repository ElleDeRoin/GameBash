using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement & Jump")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Auto Run")]
    [SerializeField] private float runSpeed = 5f;

    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
        UpdateAnimator();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
    }

    private void HandleMovement()
    {
        // Auto-run
        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0f;
        }
    }

    private void UpdateAnimator()
    {
        float yVelocity = rb.linearVelocity.y;

        // Always running
        SetAnimatorBool("isRunning", true);

        // Jumping: moving upwards & not grounded
        SetAnimatorBool("isJumping", !isGrounded && yVelocity > 0.1f);

        // Falling: moving downwards & not grounded
        SetAnimatorBool("isFalling", !isGrounded && yVelocity < -0.1f);

        // Grounded state
        SetAnimatorBool("isGrounded", isGrounded);
    }

    private void SetAnimatorBool(string param, bool value)
    {
        if (animator != null && HasParameter(param))
            animator.SetBool(param, value);
    }

    private bool HasParameter(string param)
    {
        if (animator == null) return false;

        foreach (AnimatorControllerParameter p in animator.parameters)
        {
            if (p.name == param)
                return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (feetPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(feetPos.position, groundDistance);
        }
    }
}
