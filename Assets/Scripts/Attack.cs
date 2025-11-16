using UnityEngine;

public enum AttackType
{
    High,
    Medium,
    Low
}

[RequireComponent(typeof(Collider2D))] // Ensure a Collider2D is attached
public class Attack : MonoBehaviour
{
    public AttackType attackType; // High, Medium, Low
    public int scoreValue = 1;    // Points awarded per enemy

    private void Awake()
    {
        // Make sure the collider is a trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
        else
            Debug.LogWarning("Attack requires a Collider2D component!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Determine if this enemy should be affected by this attack
        if ((attackType == AttackType.High && collision.CompareTag("HighEnemy")) ||
            (attackType == AttackType.Medium && collision.CompareTag("MediumEnemy")) ||
            (attackType == AttackType.Low && collision.CompareTag("LowEnemy")))
        {
            Destroy(collision.gameObject);               // Destroy the enemy
            ScoreManager.instance.AddScore(scoreValue); // Add points to score
        }
    }
}
