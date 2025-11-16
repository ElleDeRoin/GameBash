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

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    [Header("Auto Run")]
    [SerializeField] private float runSpeed = 5f;

    private void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimator();
    }

    private void HandleMovement()
    {
        // Always moving forward
        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
    }

    private void HandleJump()
    {
        // Jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            SetAnimatorTrigger("Jump");
        }

        // Hold jump for variable height
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

        // Stop jump on release
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0f;
        }
    }

    private void UpdateAnimator()
    {
        // Update animation parameters safely
        SetAnimatorBool("isRunning", true);
        SetAnimatorBool("isJumping", !isGrounded && rb.linearVelocity.y > 0.1f);
        SetAnimatorBool("isFalling", rb.linearVelocity.y < -0.1f && !isGrounded);
    }

    // --- Animator helper methods ---
    private void SetAnimatorBool(string param, bool value)
    {
        if (animator != null && HasParameter(param))
            animator.SetBool(param, value);
    }

    private void SetAnimatorTrigger(string param)
    {
        if (animator != null && HasParameter(param))
            animator.SetTrigger(param);
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

    // Optional: visualize ground check in Scene view
    private void OnDrawGizmosSelected()
    {
        if (feetPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(feetPos.position, groundDistance);
        }
    }
}
