using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Colliders")]
    [SerializeField] private BoxCollider2D highAttackCollider;
    [SerializeField] private BoxCollider2D mediumAttackCollider;
    [SerializeField] private BoxCollider2D lowAttackCollider;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float colliderActiveTime = 0.2f; // How long the collider is active
    private bool canAttack = true;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        // Make sure all attack colliders are disabled at start
        DisableAllColliders();
    }

    private void Update()
    {
        if (!canAttack) return;

        if (Input.GetKeyDown(KeyCode.Q))
            StartAttack(0); // High attack
        else if (Input.GetKeyDown(KeyCode.W))
            StartAttack(1); // Medium attack
        else if (Input.GetKeyDown(KeyCode.E))
            StartAttack(2); // Low attack
    }

    private void StartAttack(int attackType)
    {
        canAttack = false;

        // Reset triggers to ensure animation plays every time
        if (HasParameter("Attack"))
            animator.ResetTrigger("Attack");

        if (HasParameter("AttackType"))
            animator.SetInteger("AttackType", attackType);

        animator.SetTrigger("Attack");

        // Enable collider for a short time
        StartCoroutine(ActivateColliderTemporarily(attackType, colliderActiveTime));

        // Start cooldown
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private bool HasParameter(string param)
    {
        if (animator == null) return false;
        foreach (AnimatorControllerParameter p in animator.parameters)
            if (p.name == param)
                return true;
        return false;
    }

    private IEnumerator ActivateColliderTemporarily(int attackType, float duration)
    {
        EnableCollider(attackType);
        yield return new WaitForSeconds(duration);
        DisableAllColliders();
    }

    private void EnableCollider(int attackType)
    {
        DisableAllColliders(); // Ensure only one collider is active
        switch (attackType)
        {
            case 0: highAttackCollider.enabled = true; break;
            case 1: mediumAttackCollider.enabled = true; break;
            case 2: lowAttackCollider.enabled = true; break;
        }
    }

    private void DisableAllColliders()
    {
        highAttackCollider.enabled = false;
        mediumAttackCollider.enabled = false;
        lowAttackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (highAttackCollider.enabled && collision.CompareTag("HighEnemy"))
            Destroy(collision.gameObject);

        if (mediumAttackCollider.enabled && collision.CompareTag("MediumEnemy"))
            Destroy(collision.gameObject);

        if (lowAttackCollider.enabled && collision.CompareTag("LowEnemy"))
            Destroy(collision.gameObject);
    }
}
