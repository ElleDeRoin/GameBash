using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HighEnemy") || 
            other.CompareTag("MediumEnemy") || 
            other.CompareTag("LowEnemy"))
        {
            playerHealth.TakeDamage(1);
            Destroy(other.gameObject); // Remove enemy that passed
        }
    }
}
