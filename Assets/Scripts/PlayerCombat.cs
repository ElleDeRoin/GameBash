using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Colliders")]
    [SerializeField] private BoxCollider2D highAttackCollider;
    [SerializeField] private BoxCollider2D mediumAttackCollider;
    [SerializeField] private BoxCollider2D lowAttackCollider;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 0.3f;
    private float attackTimer = 0f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();   // Ensure Animator is on the same object
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer < attackCooldown) return;

        // HIGH ATTACK (Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("HighAttack");
            DoAttack(highAttackCollider);
        }

        // MEDIUM ATTACK (W)
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("MediumAttack");
            DoAttack(mediumAttackCollider);
        }

        // LOW ATTACK (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("LowAttack");
            DoAttack(lowAttackCollider);
        }
    }

    private void DoAttack(BoxCollider2D attackCol)
    {
        attackCol.enabled = true;
        attackTimer = 0f;
        StartCoroutine(DisableCollider(attackCol));
    }

    private System.Collections.IEnumerator DisableCollider(BoxCollider2D col)
    {
        yield return new WaitForSeconds(0.1f);
        col.enabled = false;
    }

    // Detect which enemy is hit
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
